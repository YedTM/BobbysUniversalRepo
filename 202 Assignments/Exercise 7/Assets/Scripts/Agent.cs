using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class Agent : MonoBehaviour
{
    [SerializeField]
    protected PhysicsObject myPhysicsObject;

    [SerializeField]
    protected float maxForce = 10;

    protected Vector3 wanderTarget;

    float wanderAngle;
    float perlinOffset;

    protected Vector3 totalForce;

    private AgentManager agentManager;
    protected TagManager tagManager;

    [SerializeField]
    private float separationRange;

    public AgentManager AgentManager
    {
        set
        {
            agentManager = value;
        }
    }

    public TagManager TagManager
    {
        set
        {
            tagManager = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        perlinOffset = Random.Range(0, 10000); 
        wanderAngle = Random.Range(0, Mathf.PI * 2);
    }

    // Update is called once per frame
    void Update()
    {
        totalForce = Vector3.zero;
        CalcStearingForces();
        totalForce = Vector3.ClampMagnitude(totalForce, maxForce);
        myPhysicsObject.ApplyForce(totalForce);
    }

    protected abstract void CalcStearingForces();

    protected Vector3 Seek(Vector3 targetPos)
    {
        Vector3 desiredVelocity = targetPos - transform.position;
        desiredVelocity = desiredVelocity.normalized * myPhysicsObject.MaxSpeed;
        return desiredVelocity - myPhysicsObject.Velocity;
    }

    protected Vector3 Seek(GameObject target)
    {
        return Seek(target.transform.position);
    }

    protected Vector3 Flee(Vector3 targetPos) 
    {
        Vector3 desiredVelocity = transform.position - targetPos;
        desiredVelocity = desiredVelocity.normalized * myPhysicsObject.MaxSpeed;

        return desiredVelocity - myPhysicsObject.Velocity;
    }

    protected Vector3 Flee(GameObject target) 
    {
        return Flee(target.transform.position);
    }

    protected Vector3 Wander(float time, float radius)
    {
        Vector3 futurePos = CalcFuturePosition(time);

        /*float randomAngle = Random.Range(0.0f, Mathf.PI * 2);*/
        wanderAngle += (0.5f - Mathf.PerlinNoise(transform.position.x * 0.1f + perlinOffset, transform.position.y * 0.1f + perlinOffset)) * Mathf.PI * Time.deltaTime;

        Vector3 targetPos = new Vector3(Mathf.Cos(wanderAngle) * radius, Mathf.Sin(wanderAngle) * radius);

        wanderTarget = futurePos + targetPos;
        

        return Seek(futurePos + targetPos);
    }

    protected Vector3 CalcFuturePosition(float time)
    {
        return transform.position + (myPhysicsObject.Velocity * time);
    }

    protected Vector3 BoundsForce()
    {
        if (transform.position.x < myPhysicsObject.ScreenMin.x || 
            transform.position.x > myPhysicsObject.ScreenMax.x || 
            transform.position.y < myPhysicsObject.ScreenMin.y || 
            transform.position.y > myPhysicsObject.ScreenMax.y)
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

    protected Vector3 Separate()
    {
        Vector3 separateForce = Vector3.zero;
        foreach (Agent a in agentManager.Agents)
        {
            if (a == this)
            {
                continue;
            }

            float distance = Vector3.Distance(transform.position, a.transform.position);

            separateForce += Flee(a.transform.position) * (separationRange / (distance + 0.0000000001f));
        }
        return separateForce;
    }

    protected Vector3 SeparateTag()
    {
        Vector3 separateForce = Vector3.zero;
        foreach (Agent a in tagManager.Agents)
        {
            if (a == this)
            {
                continue;
            }

            float distance = Vector3.Distance(transform.position, a.transform.position);

            separateForce += Flee(a.transform.position) * (separationRange / (distance + 0.0000000001f));
        }
        return separateForce;
    }

    //Finding nearest agent
    protected Agent FindClosest()
    {
        float minDist = Mathf.Infinity;
        Agent nearest = null;
        foreach (Agent a in tagManager.Agents) 
        { 
            if (a == this)
            {
                continue;
            }
            float dist = Vector3.Distance(this.transform.position, a.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                nearest = a;
            }
        }
        return nearest;
    }

    protected Vector3 Cohesion()
    {
        return Seek(FlockManager.Instance.CenterPoint);
    }

    protected Vector3 Alignment()
    {
        Vector3 desiredVel = FlockManager.Instance.SharedDirection * myPhysicsObject.MaxSpeed;

        return desiredVel - myPhysicsObject.Velocity;
    }
}
