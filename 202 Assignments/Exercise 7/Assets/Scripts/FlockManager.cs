using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockManager : Singleton<FlockManager>
{
    public List<Agent> flock = new List<Agent>();

    private Vector3 centerPoint = Vector3.zero;
    private Vector3 sharedDirection = Vector3.zero;

    public Vector3 CenterPoint
    {
        get
        {
            return centerPoint;
        }
    }

    public Vector3 SharedDirection
    {
        get
        {
            return sharedDirection;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        centerPoint = GetCenterPoint();
        sharedDirection = GetSharedDirection();
    }


    private Vector3 GetCenterPoint()
    {
        Vector3 sumVect = new Vector3();

        foreach (Agent agent in flock) 
        {
            sumVect += agent.transform.position;
        }

        return sumVect / flock.Count;
    }

    private Vector3 GetSharedDirection()
    {
        Vector3 sumDir = new Vector3();

        foreach (Agent agent in flock)
        {
            sumDir += agent.transform.up;
        }

        return sumDir.normalized;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(centerPoint, 0.2f);
        Gizmos.DrawLine(centerPoint, centerPoint + sharedDirection * 3);
    }
}
