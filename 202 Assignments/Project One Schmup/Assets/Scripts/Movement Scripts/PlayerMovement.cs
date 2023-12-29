using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
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
    [SerializeField]
    SpriteRenderer bulletPrefab;

    public Vector3 ObjectDirection
    {
        get
        {
            return objectDirection;
        }
    }

    public float ObjectPositionY
    {
        get
        {
            return objectPosition.y;
        }
        set
        {
            objectPosition.y = value;
        }
    }

    public float ObjectPositionX
    {
        get
        {
            return objectPosition.x;
        }
        set
        {
            objectPosition.x = value;
        }
    }

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

        // The many if statements instead of else if statements is due to 
        // the player ship being able to phase through the corners of the game window
        // by holding an x and y direction at the same time. The multiple ifs stop this
        // from happening
        if (objectDirection.y < 0 && objectPosition.y <= -mainCamera.orthographicSize)
        {
            objectPosition.y = -mainCamera.orthographicSize;
        }
        if (objectDirection.y > 0 && objectPosition.y >= mainCamera.orthographicSize)
        {
            objectPosition.y = mainCamera.orthographicSize;
        }
        if (objectDirection.x < 0 && objectPosition.x <= -totalCamWidth / 2)
        {
            objectPosition.x = -totalCamWidth / 2;
        }
        if (objectDirection.x > 0 && objectPosition.x >= totalCamWidth / 2)
        {
            objectPosition.x = totalCamWidth / 2;
        }

        transform.position = objectPosition;

    }

    public void SetDirection(Vector3 direction)
    {
        objectDirection = direction.normalized;

        if (objectDirection != Vector3.zero)
        {
            objectSpeed = 7f;
        }

    }

    public SpriteRenderer SpawnBullet()
    {
        return Instantiate(bulletPrefab, objectPosition, Quaternion.identity);
    }




}
