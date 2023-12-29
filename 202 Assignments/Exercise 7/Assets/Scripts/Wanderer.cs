using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Wanderer : Agent
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
    [Range(0.0f, 100.0f)]
    float separateWeight = 10.0f;

    [SerializeField]
    [Range(1.0f, 100.0f)]
    float cohesionWeight = 10.0f;

    [SerializeField]
    [Range(1.0f, 100.0f)]
    float alignmentWeight = 10.0f;



    protected override void CalcStearingForces()
    {
        totalForce += Wander(wanderTime, wanderRadius) * wanderWeight;
        totalForce += BoundsForce() * boundsWeight;
        totalForce += Separate() * separateWeight;
        totalForce += Cohesion() * cohesionWeight;
        totalForce += Alignment() * alignmentWeight;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(CalcFuturePosition(wanderTime), wanderRadius);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, wanderTarget);
    }
}
