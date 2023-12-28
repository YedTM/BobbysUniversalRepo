using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateHand : MonoBehaviour
{
    //Variables
    float turnAmount;
    [SerializeField]
    bool useDeltaTime;

    // Start is called before the first frame update
    void Start()
    {
        turnAmount = -0.60f;
    }

    // Update is called once per frame
    void Update()
    {
        if (useDeltaTime)
        {
            transform.Rotate(0, 0, turnAmount * Time.deltaTime * 10);
        }
        else
        {
            transform.Rotate(0, 0, turnAmount);
        }


    }
}
