using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seeker : Agent
{
    [SerializeField]
    GameObject target;

    Vector3 seekForce;



    protected override void CalcStearingForces()
    {
        seekForce = Seek(target);
        totalForce += seekForce;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawLine(transform.position, transform.position + myPhysicsObject.Velocity);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + (seekForce * 0.3f));

        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, myPhysicsObject.Radius);
    }
}
