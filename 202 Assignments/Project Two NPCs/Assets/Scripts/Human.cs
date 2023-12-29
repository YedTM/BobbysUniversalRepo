using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Human : Agent
{
    [SerializeField]
    float wanderTime = 1f;
    [SerializeField]
    float wanderRadius = 1f;
    [SerializeField]
    [Range(1.0f, 100.0f)]
    float boundsWeight = 10.0f;

    [SerializeField]
    [Range(1.0f, 100.0f)]
    float wanderWeight = 10.0f;

    [SerializeField]
    [Range(1.0f, 100.0f)]
    float separateWeight = 10.0f;

    [SerializeField]
    float avoidTime = 1.0f;

    [SerializeField]
    [Range(1.0f, 100.0f)]
    float avoidWeight = 1.0f;

    [SerializeField]
    [Range(1.0f, 20.0f)]
    float detectionRadius = 1.0f;

    [SerializeField]
    Sprite wanderSprite;

    [SerializeField]
    Sprite fleeSprite;

    [SerializeField]
    Zombie zombiePrefab;

    [SerializeField]
    [Range(1.0f, 100.0f)]
    float fleeWeight = 1.0f;

    /// <summary>
    /// This method overrides the CalcStearingForces method from the Agent class.
    /// The human has a bounds force, separation force, and avoidance force applied constantly.
    /// The ZombieInRange and FleerCollided methods are called every update, which can apply a flee force
    /// depending on the circumstances.
    /// </summary>
    protected override void CalcStearingForces()
    {

        ZombieInRange();
        totalForce += BoundsForce() * boundsWeight;
        totalForce += Separate(this) * separateWeight;
        totalForce += AvoidObstacles(avoidTime) * avoidWeight;
        FleerCollided();

    }

    /// <summary>
    /// If a zombie enters the detection radius of the human, the human will change its sprite
    /// to the flee sprite and will then flee the zombie to the best of its ability.
    /// Otherwise, it will wander and use the wander sprite.
    /// </summary>
    private void ZombieInRange()
    {
        foreach (Zombie z in AgentManager.Zombies) 
        {
            if (Mathf.Pow(transform.position.x - z.transform.position.x, 2) +
                Mathf.Pow(transform.position.y - z.transform.position.y, 2) <=
                Mathf.Pow(detectionRadius + z.MyPhysicsObject.Radius, 2))
            {
                spriteRenderer.sprite = fleeSprite;
                totalForce += Flee(z.gameObject) * fleeWeight;
                break;
            }
            else
            {
                spriteRenderer.sprite = wanderSprite;
                totalForce += Wander(wanderTime, wanderRadius) * wanderWeight;
            }
            
        }
    }

    /// <summary>
    /// If the human agent intersects with a zombie agent, it will create a new zombie agent
    /// in its place and will destroy itself.
    /// </summary>
    private void FleerCollided()
    {
        foreach (Zombie z in AgentManager.Zombies)
        {
            if (Mathf.Pow(transform.position.x - z.transform.position.x, 2) +
                Mathf.Pow(transform.position.y - z.transform.position.y, 2) <=
                Mathf.Pow(myPhysicsObject.Radius + z.MyPhysicsObject.Radius, 2))
            {
                Zombie zombie = Instantiate(zombiePrefab, transform.position, Quaternion.identity);
                zombie.AgentManager = AgentManager;
                AgentManager.Zombies.Add(zombie);
                this.AgentManager.Humans.Remove(this);
                Destroy(this.gameObject);
                break;
            }
        }
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(CalcFuturePosition(wanderTime), wanderRadius);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, wanderTarget);

        Gizmos.color = Color.gray;
        Vector3 futurePosition = CalcFuturePosition(avoidTime);
        float dist = Vector3.Distance(transform.position, futurePosition) + myPhysicsObject.Radius;
        Vector3 boxSize = new Vector3(myPhysicsObject.Radius * 2, dist, myPhysicsObject.Radius * 2);

        Vector3 center = new Vector3(0, dist / 2, 0);

        Gizmos.matrix = transform.localToWorldMatrix;

        Gizmos.DrawWireCube(center, boxSize);

        Gizmos.matrix = Matrix4x4.identity;

        Gizmos.color = Color.blue;
        foreach (Vector3 pos in locationOfObstacles) 
        {
            Gizmos.DrawLine(transform.position, pos);
        }

        

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(myPhysicsObject.transform.position, detectionRadius);

    }
}
