using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Fleer : Agent
{
    [SerializeField]
    PhysicsObject target;

    Vector3 fleeForce;

    protected override void CalcStearingForces()
    {
        fleeForce = Flee(target.gameObject);
        totalForce += fleeForce;
        if (FleerCollided(target))
        {
            float randomY = Random.Range(myPhysicsObject.ScreenMin.y, myPhysicsObject.ScreenMax.y);
            float randomX = Random.Range(myPhysicsObject.ScreenMin.x, myPhysicsObject.ScreenMax.x);
            myPhysicsObject.Position = new Vector2(randomX,randomY);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawLine(transform.position, transform.position + myPhysicsObject.Velocity);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + (fleeForce * 0.3f));

        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, myPhysicsObject.Radius);
    }

    private bool FleerCollided(PhysicsObject target)
    {
        return Mathf.Pow(transform.position.x - target.transform.position.x, 2) +
               Mathf.Pow(transform.position.y - target.transform.position.y, 2) <=
               Mathf.Pow(myPhysicsObject.Radius + target.Radius, 2);
    }
}
