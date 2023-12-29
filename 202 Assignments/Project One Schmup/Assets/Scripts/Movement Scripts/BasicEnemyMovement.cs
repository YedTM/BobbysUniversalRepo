using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyMovement : MonoBehaviour
{
    //Variables
    Vector3 objectPosition = Vector3.zero;
    [SerializeField]
    float objectSpeed = 4f;
    Vector3 objectDirection = new Vector3(-1, -1, 0);
    Vector3 objectVelocity = Vector3.zero;
    [SerializeField]
    Camera mainCamera;
    float totalCamHeight;
    float totalCamWidth;
    [SerializeField]
    SpriteRenderer bulletPrefab;
    float timePassed = 0;

    private SpriteRenderer SpawnBullet()
    {
        return Instantiate(bulletPrefab, objectPosition, Quaternion.identity);
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

        if (objectPosition.x < 3 )
        {
            objectPosition.x = 3;

            if (Random.Range(0f, 1f) < 0.5f)
            {
                objectDirection = Vector3.up;
            }
            else
            {
                objectDirection = Vector3.down;
            }
        }
        if (objectPosition.x <= totalCamWidth / 2)
        {
            timePassed += Time.deltaTime;
            if (timePassed >= 1.75f)
            {
                timePassed = 0;
                SpawnBullet();
            }
        }

        if (objectPosition.y >= mainCamera.orthographicSize)
        {
            objectDirection.y = -1;
        }
        else if (objectPosition.y <= -mainCamera.orthographicSize)
        {
            objectDirection.y = 1;
        }

        transform.position = objectPosition;

        
    }
}
