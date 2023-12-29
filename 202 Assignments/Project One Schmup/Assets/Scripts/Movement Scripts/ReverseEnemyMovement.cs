using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseEnemyMovement : MonoBehaviour
{
    //Variables
    Vector3 objectPosition = Vector3.zero;
    [SerializeField]
    float objectSpeed = 3.5f;
    Vector3 objectDirection = Vector3.left;
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
        return Instantiate(bulletPrefab, objectPosition, Quaternion.Euler(0, 180, 0));
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

        if (objectPosition.x <= totalCamWidth / 2)
        {
            timePassed += Time.deltaTime;
            if (timePassed >= 2.5f)
            {
                timePassed = 0;
                SpawnBullet();
            }
        }
        if (objectPosition.x < 8)
        {
            objectPosition.x = 8;
            objectSpeed = 0;
        }

        transform.position = objectPosition;
    }
}
