using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedsterEnemyMovement : MonoBehaviour
{
    //Variables
    Vector3 objectPosition = Vector3.zero;
    [SerializeField]
    float objectSpeed = 12f;
    Vector3 objectDirection = Vector3.left;
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

        if (objectPosition.x <= -totalCamWidth / 2)
        {
            objectPosition.x = totalCamWidth / 2;
        }

        transform.position = objectPosition;
    }
}
