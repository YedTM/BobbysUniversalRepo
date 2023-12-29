using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBuiltMovement : MonoBehaviour
{
    //Variables
    Vector3 objectPosition = Vector3.zero;
    float objectSpeed = 15f;
    Vector3 objectDirection = Vector3.left;
    Vector3 objectVelocity = Vector3.zero;
    [SerializeField]
    Camera mainCamera;
    float totalCamHeight;

    // Start is called before the first frame update
    void Start()
    {
        objectPosition = transform.position;
        mainCamera = Camera.main;
        totalCamHeight = mainCamera.orthographicSize * 2f;
    }

    // Update is called once per frame
    void Update()
    {
        objectVelocity = objectDirection * objectSpeed * Time.deltaTime;

        objectPosition += objectVelocity;

        if (objectPosition.x < -3.5)
        {
            objectPosition.x = -3.5f;
            objectSpeed = 0.5f;

            objectDirection = Vector3.up;

        }

        if (objectPosition.y >= mainCamera.orthographicSize)
        {
            objectDirection = Vector3.down;
        }
        else if (objectPosition.y <= -mainCamera.orthographicSize)
        {
            objectDirection = Vector3.up;
        }

        transform.position = objectPosition;
    }
}
