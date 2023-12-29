using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallEnemyMovement : MonoBehaviour
{
    //Variables
    Vector3 objectPosition = Vector3.zero;
    float objectSpeed = 5f;
    Vector3 objectDirection = Vector3.left;
    Vector3 objectVelocity = Vector3.zero;
    [SerializeField]
    Camera mainCamera;
    float totalCamHeight;
    [SerializeField]
    SpriteRenderer wallBuilt;
    float totalCamWidth;
    bool hasWall;

    private SpriteRenderer SpawnWall()
    {
        return Instantiate(wallBuilt, objectPosition, Quaternion.identity);
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

        if (objectPosition.x < 6)
        {
            objectPosition.x = 6;
            objectSpeed = 0.5f;

            objectDirection = Vector3.up;
        }

        if (objectPosition.x <= totalCamWidth / 2 && hasWall == false)
        {
            SpawnWall();
            hasWall = true;
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
