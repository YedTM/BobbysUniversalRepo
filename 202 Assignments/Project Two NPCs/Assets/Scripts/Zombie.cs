using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Zombie : Agent
{
    public Human target;

    [SerializeField]
    AgentManager targetManager;

    [SerializeField]
    [Range(1.0f, 100.0f)]
    float boundsWeight = 10.0f;

    [SerializeField]
    float wanderTime = 1f;
    [SerializeField]
    float wanderRadius = 1f;
    [SerializeField]
    [Range(1.0f, 100.0f)]
    float wanderWeight = 10.0f;

    [SerializeField]
    [Range(1.0f, 20.0f)]
    float detectionRadius = 1.0f;

    [SerializeField]
    Sprite wanderSprite;

    [SerializeField]
    Sprite seekSprite;

    [SerializeField]
    float avoidTime = 1.0f;

    [SerializeField]
    [Range(1.0f, 100.0f)]
    float avoidWeight = 1.0f;

    [SerializeField]
    [Range(1.0f, 100.0f)]
    float distractionWeight = 1.0f;

    [SerializeField]
    [Range(10.0f, 1000.0f)]
    float separateWeight = 10.0f;

    /// <summary>
    /// This method overrides the Agent's CalcStearingForces in order to apply forces
    /// to the Zombie agent. The zombie has a bounds force, separation force, and avoidance force that are
    /// constantly applied. Seek forces may also be applied, but are dependent on different factors.
    /// </summary>
    protected override void CalcStearingForces()
    {
        totalForce += BoundsForce() * boundsWeight;
        totalForce += Separate(this) * separateWeight;
        totalForce += AvoidObstacles(avoidTime) * avoidWeight;

        // The human in range method is called in order to seek a human agent
        HumanInRange();

        // If a distaction exists in the scene, then the zombie will seek it
        if(AgentManager.Distraction != null)
        {
            totalForce += Seek(AgentManager.Distraction.transform.position) * distractionWeight;
            DistractionCollision();
        }

    }

    /// <summary>
    /// If a human is within the detection radius of the zombie, then the zombie will seek
    /// the human and its sprite will change to its seek sprite. Otherwise, it will wander 
    /// and use its wander sprite
    /// </summary>
    private void HumanInRange()
    {
        foreach(Human h in AgentManager.Humans)
        {
            if (Mathf.Pow(transform.position.x - h.transform.position.x, 2) +
                Mathf.Pow(transform.position.y - h.transform.position.y, 2) <=
                Mathf.Pow(detectionRadius + h.MyPhysicsObject.Radius, 2))
            {
                spriteRenderer.sprite = seekSprite;
                totalForce += Seek(h.gameObject) * 5;
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
    /// If the zombie intersects the distraction object, it will destroy the object
    /// </summary>
    private void DistractionCollision()
    {
        if (Mathf.Pow(transform.position.x - AgentManager.Distraction.transform.position.x, 2) +
            Mathf.Pow(transform.position.y - AgentManager.Distraction.transform.position.y, 2) <=
            Mathf.Pow(myPhysicsObject.Radius + AgentManager.Distraction.radius, 2))
        {
            Destroy(AgentManager.Distraction.gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawLine(transform.position, transform.position + myPhysicsObject.Velocity);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, myPhysicsObject.Radius);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(myPhysicsObject.transform.position, detectionRadius);
    }
}
