using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    //Variables
    [SerializeField]
    GameObject creaturePrefab;

    int xPosition;
    float yPosition;
    int zPosition;
    Vector3 creatureLocation;


    // Start is called before the first frame update
    void Start()
    {
        xPosition = 5;
        yPosition = -0.8f;
        zPosition = 15;
        creatureLocation = new Vector3(xPosition, yPosition, zPosition);
        Instantiate(creaturePrefab, creatureLocation, Quaternion.Euler(0,-255,0));

        xPosition = -15;
        zPosition = 25;
        creatureLocation = new Vector3(xPosition, yPosition, zPosition);
        Instantiate(creaturePrefab, creatureLocation, Quaternion.Euler(0, -280, 0));

        xPosition = 15;
        zPosition = 20;
        creatureLocation = new Vector3(xPosition, yPosition, zPosition);
        Instantiate(creaturePrefab, creatureLocation, Quaternion.Euler(0, -240, 0));

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
