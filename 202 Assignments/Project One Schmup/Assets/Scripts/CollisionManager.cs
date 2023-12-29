using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CollisionManager : MonoBehaviour
{
    [SerializeField]
    List<SpriteInfo> collidables = new List<SpriteInfo>();
    [SerializeField]
    PlayerMovement playerShip;
    int playerShipNumber;
    int playerLives;
    int playerScore;
    [SerializeField]
    Text scoreText;
    [SerializeField]
    Text livesText;
    int scoreCounter;
    bool livesIncreased;
    int currentLevel;
    public int enemiesDestroyed;
    [SerializeField]
    Text gameOverText;
    bool playerDead;
    int finalScore;

    public int CurrentLevel
    {
        get 
        { 
            return currentLevel; 
        }
    }

    public int EnemiesDestroyed
    {
        get
        {
            return enemiesDestroyed;
        }
    }

    public int PlayerLives
    {
        get
        {
            return playerLives;
        }
    }

    public int PlayerScore
    {
        get
        {
            return playerScore;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        collidables = new List<SpriteInfo>(GameObject.FindObjectsOfType<SpriteInfo>());
        playerLives = 5;
        playerScore = 0;
        livesIncreased = false;
        currentLevel = 1;
        enemiesDestroyed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: " + playerScore;
        livesText.text = "Lives: " + playerLives;

        if (currentLevel * 10 <= enemiesDestroyed && playerDead == false)
        {
            currentLevel++;
            enemiesDestroyed = 0;
        }

        collidables = new List<SpriteInfo>(GameObject.FindObjectsOfType<SpriteInfo>());
        
        for (int i = 0; i < collidables.Count; i++)
        {
            if (collidables[i] == null)
            {
                collidables.RemoveAt(i);
            }
        }

        playerShipNumber = collidables.Count - 1;

        //Set all isColliding to false each frame
        for (int i = 0; i < collidables.Count; i++)
        {
            collidables[i].IsColliding = false;
        }

        for (int i = 0; i < collidables.Count; i++)
        {
            if (collidables[i] != null)
            {
                if (AABBCheck(collidables[playerShipNumber], collidables[i])
                && playerShipNumber != i
                && collidables[i].name != "Player-Missile(Clone)"
                && collidables[i].name != "Wall-Built(Clone)")
                {
                    collidables[playerShipNumber].IsColliding = true;
                    playerLives--;
                    Destroy(collidables[i].gameObject);
                    if (collidables[i].name != "Enemy-Missile(Clone)"
                       & collidables[i].name != "Backward-Missile(Clone)")
                    {
                        enemiesDestroyed++;
                    }
                }
                else if (AABBCheck(collidables[playerShipNumber], collidables[i])
                         && playerShipNumber != i
                         && collidables[i].name != "Player-Missile(Clone)"
                         && collidables[i].name == "Wall-Built(Clone)")
                {
                    if (playerShip.ObjectPositionY > collidables[i].RectMax.y) 
                    {
                        playerShip.ObjectPositionY = collidables[i].RectMax.y + 0.5f;
                    }
                    if (playerShip.ObjectPositionY < collidables[i].RectMin.y)
                    {
                        playerShip.ObjectPositionY = collidables[i].RectMin.y - 0.5f;
                    }
                    if (playerShip.ObjectDirection.x < 0)
                    {
                        playerShip.ObjectPositionX = collidables[i].RectMax.x + 1f;
                    }
                    if (playerShip.ObjectDirection.x > 0)
                    {
                        playerShip.ObjectPositionX = collidables[i].RectMin.x - 1f;
                    }
                }
                if (collidables[i] != null)
                {
                    for (int j = 0; j < collidables.Count; j++)
                    {
                        if (collidables[j] != null)
                        {
                            if (AABBCheck(collidables[i], collidables[j])
                            && collidables[i].name == "Player-Missile(Clone)"
                            && collidables[j].name != "Player-Missile(Clone)"
                            && collidables[j].name != "Player Ship"
                            && collidables[j].name != "Enemy-Missile(Clone)"
                            && collidables[j].name != "Backward-Missile(Clone)"
                            && collidables[j].name != "Wall-Built(Clone)")
                            {
                                if (collidables[j].name == "Basic-Enemy(Clone)")
                                {
                                    playerScore += 10;
                                    scoreCounter += 10;
                                }
                                else if (collidables[j].name == "Speedster(Clone)")
                                {
                                    playerScore += 15;
                                    scoreCounter += 15;
                                }
                                else if (collidables[j].name == "Reverse-Shooter(Clone)")
                                {
                                    playerScore += 20;
                                    scoreCounter += 20;
                                }
                                else if (collidables[j].name == "Wall-Builder(Clone)")
                                {
                                    playerScore += 25;
                                    scoreCounter += 25;
                                    for (int k = j-1; k >= 0; k--)
                                    {
                                        if (collidables[k].name == "Wall-Built(Clone)")
                                        {
                                            Destroy(collidables[k].gameObject);
                                            break;
                                        }
                                    }
                                    
                                }

                                enemiesDestroyed++;
                                Destroy(collidables[i].gameObject);
                                Destroy(collidables[j].gameObject);
                            }
                            else if (AABBCheck(collidables[i], collidables[j])
                                && collidables[i].name == "Player-Missile(Clone)"
                                && collidables[j].name == "Wall-Built(Clone)")
                            {
                                Destroy(collidables[i].gameObject);
                            }
                        }

                    }
                }
                
            }

            
        }

        if (playerScore != 0 && scoreCounter >= 200 && livesIncreased == false) 
        {
            playerLives++;
            livesIncreased = true;
            scoreCounter -= 200;
        }
        else
        {
            livesIncreased = false;
        }

        if (playerLives == 0 && playerDead == false)
        {
            for (int i = 0; i < collidables.Count; i++)
            {
                Destroy(collidables[i].gameObject);
            }
            playerDead = true;
            currentLevel = 0;
            finalScore = playerScore;
            playerScore = 0;
        }

        if (playerDead)
        {
            gameOverText.text = "Game Over\r\nPress Refresh\r\nTo Play" +
                "\r\nAgain!\r\nYour Final\r\nScore Was:\r\n" + finalScore;
        }
    }

    bool AABBCheck(SpriteInfo spriteA, SpriteInfo spriteB)
    {
        return spriteB.RectMin.x < spriteA.RectMax.x && spriteB.RectMax.x > spriteA.RectMin.x &&
               spriteB.RectMin.y < spriteA.RectMax.y && spriteB.RectMax.y > spriteA.RectMin.y;
    }

}
