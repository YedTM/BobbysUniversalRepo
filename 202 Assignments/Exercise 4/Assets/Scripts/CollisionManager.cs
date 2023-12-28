using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

public class CollisionManager : MonoBehaviour
{
    [SerializeField]
    List<SpriteInfo> collidables = new List<SpriteInfo>();
    [SerializeField]
    bool isAABB = true;

    [SerializeField]
    TextMeshPro detectionText;


    // Start is called before the first frame update
    void Start()
    {
        collidables = new List<SpriteInfo>(GameObject.FindObjectsOfType<SpriteInfo>());
    }

    // Update is called once per frame
    void Update()
    {
        ChangeCollisionType();

        //Set all isColliding to false each frame
        for (int i = 0; i < collidables.Count; i++)
        {
            collidables[i].IsColliding = false;
        }
        
        for (int i = 0; i < collidables.Count; i++)
        {
            for (int j = i+1; j < collidables.Count; j++)
            {
                if (isAABB)
                {
                    detectionText.text = "Press Space to Change Collision Detection\r\nCurrent Detection: AABB";

                    if (AABBCheck(collidables[i], collidables[j]))
                    {
                        collidables[i].IsColliding = true;
                        collidables[j].IsColliding = true;
                    }
                }
                else
                {
                    detectionText.text = "Press Space to Change Collision Detection\r\nCurrent Detection: Circle";

                    if (CircleCollision(collidables[i], collidables[j]))
                    {
                        collidables[i].IsColliding = true;
                        collidables[j].IsColliding = true;
                    }
                }
                
            }
        }
        
    }

    bool AABBCheck(SpriteInfo spriteA, SpriteInfo spriteB)
    {
        return spriteB.RectMin.x < spriteA.RectMax.x && spriteB.RectMax.x > spriteA.RectMin.x &&
               spriteB.RectMin.y < spriteA.RectMax.y && spriteB.RectMax.y > spriteA.RectMin.y;
    }

    bool CircleCollision(SpriteInfo spriteA, SpriteInfo spriteB)
    {
        return Mathf.Pow(spriteA.transform.position.x - spriteB.transform.position.x, 2) + 
               Mathf.Pow(spriteA.transform.position.y -  spriteB.transform.position.y, 2) <=
               Mathf.Pow(spriteA.Radius + spriteB.Radius, 2);
    }

    void ChangeCollisionType()
    {
        if (Keyboard.current.spaceKey.wasReleasedThisFrame == true)
        {
            if (isAABB)
            {
                isAABB = false;
            }
            else
            {
                isAABB = true;
            }
        }
    }

}
