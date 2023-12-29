using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer basicEnemy;
    [SerializeField]
    SpriteRenderer reverseEnemy;
    [SerializeField]
    SpriteRenderer shieldEnemy;
    [SerializeField]
    SpriteRenderer fastEnemy;
    float timePassed = 0;
    Camera mainCamera;
    float totalCamHeight;
    float spawnChance;
    [SerializeField]
    SpriteInfo playerInfo;
    [SerializeField]
    Text levelText;
    [SerializeField]
    CollisionManager levelReader;
    int amountCreated;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        totalCamHeight = mainCamera.orthographicSize * 2f;
    }

    // Update is called once per frame
    void Update()
    {
        levelText.text = "Level: " + levelReader.CurrentLevel;

        if (amountCreated < levelReader.CurrentLevel * 10 && levelReader.CurrentLevel != 0)
        {
            timePassed += Time.deltaTime;
            if (timePassed >= 1f)
            {
                amountCreated++;
                timePassed = 0;
                spawnChance = Random.Range(0f, 1f);
                if (spawnChance < 0.5)
                {
                    Instantiate(basicEnemy,
                            new Vector2(Random.Range(15f, 30f),
                                        Random.Range(-mainCamera.orthographicSize + 2,
                                                     mainCamera.orthographicSize - 2)),
                            Quaternion.identity);
                }
                else if (spawnChance < 0.7)
                {
                    Instantiate(reverseEnemy,
                            new Vector2(Random.Range(15f, 30f),
                                        Random.Range(-mainCamera.orthographicSize + 2,
                                                     mainCamera.orthographicSize - 2)),
                            Quaternion.identity);
                }
                else if (spawnChance < 0.9)
                {
                    Instantiate(fastEnemy,
                            new Vector2(Random.Range(15f, 30f),
                                        playerInfo.RectMax.y),
                            Quaternion.identity);
                }
                else
                {
                    Instantiate(shieldEnemy,
                            new Vector2(Random.Range(15f, 30f),
                                        Random.Range(-mainCamera.orthographicSize + 2,
                                                     mainCamera.orthographicSize - 2)),
                            Quaternion.identity);
                }
            }
        }
        else if (levelReader.EnemiesDestroyed == levelReader.CurrentLevel * 10)
        {
            amountCreated = 0;
        }
    }
}
