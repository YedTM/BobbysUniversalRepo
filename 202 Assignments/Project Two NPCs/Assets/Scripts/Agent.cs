using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Properties;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public abstract class Agent : MonoBehaviour
{
    [SerializeField]
    protected SpriteRenderer spriteRenderer;

    [SerializeField]
    protected PhysicsObject myPhysicsObject;

    [SerializeField]
    protected float maxForce = 10;

    protected Vector3 wanderTarget;

    float wanderAngle;
    float perlinOffset;

    protected Vector3 totalForce;

    private AgentManager agentManager;

    [SerializeField]
    private float separationRange;

    protected List<Vector3> locationOfObstacles = new List<Vector3>();

    public AgentManager AgentManager
    {
        get
        {
            return agentManager;
        }
        set
        {
            agentManager = value;
        }
    }

    public PhysicsObject MyPhysicsObject
    {
        get
        {
            return myPhysicsObject;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        perlinOffset = Random.Range(0, 10000);
        wanderAngle = Random.Range(0, Mathf.PI * 2);
    }

    // Update is called once per frame
    // CalcStearingForces is called every update and will apply the force to the object
    void Update()
    {
        totalForce = Vector3.zero;
        CalcStearingForces();
        totalForce = Vector3.ClampMagnitude(totalForce, maxForce);
        myPhysicsObject.ApplyForce(totalForce);
    }

    protected abstract void CalcStearingForces();

    /// <summary>
    /// The object will use velocity to head in the direction of a desired location
    /// </summary>
    /// <param name="targetPos">The desired location</param>
    /// <returns>The force needed to head towards the target position</returns>
    protected Vector3 Seek(Vector3 targetPos)
    {
        Vector3 desiredVelocity = targetPos - transform.position;
        desiredVelocity = desiredVelocity.normalized * myPhysicsObject.MaxSpeed;
        return desiredVelocity - myPhysicsObject.Velocity;
    }

    /// <summary>
    /// This seeks a game object's position, using the previous Seek method 
    /// to do so
    /// </summary>
    /// <param name="target">The desired game object</param>
    /// <returns>The force needed to head towards the target position</returns>
    protected Vector3 Seek(GameObject target)
    {
        return Seek(target.transform.position);
    }

    /// <summary>
    /// The object will use velocity to run away from a desired location
    /// </summary>
    /// <param name="targetPos">The desired location</param>
    /// <returns>The force needed to flee the position</returns>
    protected Vector3 Flee(Vector3 targetPos)
    {
        Vector3 desiredVelocity = transform.position - targetPos;
        desiredVelocity = desiredVelocity.normalized * myPhysicsObject.MaxSpeed;

        return desiredVelocity - myPhysicsObject.Velocity;
    }

    /// <summary>
    /// This flees a game object's position, using the previous Flee method
    /// </summary>
    /// <param name="target">The desired game object</param>
    /// <returns>The force needed to flee the position</returns>
    protected Vector3 Flee(GameObject target)
    {
        return Flee(target.transform.position);
    }

    /// <summary>
    /// The agent will use perlin noise to wander around the game scene using 
    /// a future position and radius that determines orientation
    /// </summary>
    /// <param name="time">How far ahead the future position will be</param>
    /// <param name="radius">The radius used to determine the orientation of its movement</param>
    /// <returns>A seek vector of the desired wander location</returns>
    protected Vector3 Wander(float time, float radius)
    {
        Vector3 futurePos = CalcFuturePosition(time);

        wanderAngle += (0.5f - Mathf.PerlinNoise(transform.position.x * 0.1f + perlinOffset, transform.position.y * 0.1f + perlinOffset)) * Mathf.PI * Time.deltaTime;

        Vector3 targetPos = new Vector3(Mathf.Cos(wanderAngle) * radius, Mathf.Sin(wanderAngle) * radius);

        wanderTarget = futurePos + targetPos;


        return Seek(futurePos + targetPos);
    }

    protected Vector3 CalcFuturePosition(float time)
    {
        return transform.position + (myPhysicsObject.Velocity * time);
    }

    /// <summary>
    /// This method moves agents away from the scene window borders to avoid
    /// the agent moving off the screen
    /// </summary>
    /// <returns>The force that moves the agent away from the window borders</returns>
    protected Vector3 BoundsForce()
    {
        if (transform.position.x < myPhysicsObject.ScreenMin.x + 1 ||
            transform.position.x > myPhysicsObject.ScreenMax.x - 1 ||
            transform.position.y < myPhysicsObject.ScreenMin.y + 1 ||
            transform.position.y > myPhysicsObject.ScreenMax.y - 1)
        {
            Vector3 cameraPosition = Camera.main.transform.position;
            cameraPosition.z = transform.position.z;
            return Seek(cameraPosition);
        }
        else
        {
            return Vector3.zero;
        }
    }

    /// <summary>
    /// The human agent will move away from other humans it is near
    /// </summary>
    /// <param name="human">This agent</param>
    /// <returns>The force moving the agent away from the other humans</returns>
    protected Vector3 Separate(Human human)
    {
        Vector3 separateForce = Vector3.zero;
        foreach (Human h in agentManager.Humans)
        {
            if (h == human)
            {
                continue;
            }

            float distance = Vector3.Distance(transform.position, h.transform.position);

            separateForce += Flee(h.transform.position) * (separationRange / (distance + 0.0000000001f));
        }
        return separateForce;
    }

    /// <summary>
    /// The zombie agent will move away from other zombies it is near
    /// </summary>
    /// <param name="zombie">This agent</param>
    /// <returns>The force moving this agent away from other zombies</returns>
    protected Vector3 Separate(Zombie zombie)
    {
        Vector3 separateForce = Vector3.zero;
        foreach (Zombie z in agentManager.Zombies)
        {
            if (z == zombie)
            {
                continue;
            }

            float distance = Vector3.Distance(transform.position, z.transform.position);

            separateForce += Flee(z.transform.position) * (separationRange / (distance + 0.0000000001f));
        }
        return separateForce;
    }

    /// <summary>
    /// This method is used to avoid multiple obstacles in a scene. 
    /// The agent will take into account the radius of the obstacle and will determine if it 
    /// is to the left or right of the object, and will move to the correct side depending on its
    /// position.
    /// </summary>
    /// <param name="avoidTime">The time it takes to reach a future position</param>
    /// <returns>A force that moves an agent away from the obstacle</returns>
    protected Vector3 AvoidObstacles(float avoidTime)
    {
        Vector3 avoidForce = Vector3.zero;
        locationOfObstacles.Clear();
        Vector3 futurePosition = CalcFuturePosition(avoidTime);
        float maxDist = Vector3.Distance(transform.position, futurePosition) + myPhysicsObject.Radius;

        ///Detect and avoid obstacles 
        foreach (Obstacle ob in agentManager.obstacles) 
        { 
            Vector3 agentToObst = ob.transform.position - transform.position;
            float forwardDot = Vector3.Dot(agentToObst, transform.up);
            float rightDot = Vector3.Dot(agentToObst, transform.right);

            if (forwardDot >= -ob.radius && forwardDot <= (maxDist + ob.radius) && Mathf.Abs(rightDot) <= ((myPhysicsObject.Radius * 1.5f) + ob.radius) )
            {
                locationOfObstacles.Add(ob.transform.position);

                if (rightDot > 0)
                {
                    avoidForce += (transform.right * -1) / Mathf.Abs(forwardDot / maxDist);
                }
                else
                {
                    avoidForce += transform.right / Mathf.Abs((forwardDot / maxDist));
                }
            }
            
        }
        return avoidForce;
    }
}
