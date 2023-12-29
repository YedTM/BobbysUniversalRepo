using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseBullet : MonoBehaviour
{
    //Variables
    Vector3 objectPosition = Vector3.zero;
    float objectSpeed = 4.5f;
    Vector3 objectDirection = Vector3.right;
    Vector3 objectVelocity = Vector3.zero;
    Camera mainCamera;
    float totalCamHeight;
    float totalCamWidth;
    bool hasWrapped;

    // Start is called before the first frame update
    void Start()
    {
        objectPosition = transform.position;
        mainCamera = Camera.main;
        totalCamHeight = mainCamera.orthographicSize * 2f;
        totalCamWidth = totalCamHeight * mainCamera.aspect;
        hasWrapped = false;
    }

    // Update is called once per frame
    void Update()
    {
        objectVelocity = objectDirection * objectSpeed * Time.deltaTime;

        objectPosition += objectVelocity;

        if (objectPosition.x >= totalCamWidth / 2 && hasWrapped == false)
        {
            objectPosition.x = -totalCamWidth / 2;
            hasWrapped = true;
        }
        else if (objectPosition.x >= totalCamWidth / 2 && hasWrapped == true)
        {
            Destroy(this.gameObject);
        }

        transform.position = objectPosition;
    }
}
