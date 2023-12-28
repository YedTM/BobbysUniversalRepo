using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    //Variables
    Vector3 objectPosition = Vector3.zero;
    [SerializeField]
    float objectSpeed = 0f;
    Vector3 objectDirection = Vector3.up;
    Vector3 objectVelocity = Vector3.zero;
    [SerializeField]
    Camera mainCamera;
    float totalCamHeight;
    float totalCamWidth;


    // Start is called before the first frame update
    void Start()
    {
        objectPosition = transform.position;
        mainCamera = Camera.main;
        totalCamHeight = mainCamera.orthographicSize * 2f;
        totalCamWidth = totalCamHeight * mainCamera.aspect;
    }

    // Update is called once per frame
    void Update()
    {
        objectVelocity = objectDirection * objectSpeed * Time.deltaTime;

        objectPosition += objectVelocity;

        //This is where wrapping will occur when you do it
        if (objectDirection.y < 0 && objectPosition.y <= -mainCamera.orthographicSize)
        {
            objectPosition.y = mainCamera.orthographicSize;
        }
        else if (objectDirection.y > 0 && objectPosition.y >= mainCamera.orthographicSize)
        {
            objectPosition.y = -mainCamera.orthographicSize;
        }
        else if (objectDirection.x < 0 && objectPosition.x <= -totalCamWidth/2)
        {
            objectPosition.x = totalCamWidth/2;
        }
        else if (objectDirection.x > 0 && objectPosition.x >= totalCamWidth/2)
        {
            objectPosition.x = -totalCamWidth/2;
        }

        transform.position = objectPosition;
    }

    public void SetDirection(Vector3 direction)
    {
        objectDirection = direction.normalized;

        if (objectDirection != Vector3.zero) 
        {
            objectSpeed = 8f;
            transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
        }
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawLine(objectPosition, objectDirection * 3 + objectPosition);
    }
}
