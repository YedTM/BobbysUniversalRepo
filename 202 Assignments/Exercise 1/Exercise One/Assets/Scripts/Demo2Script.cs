using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demo2Script : MonoBehaviour
{
    [SerializeField]
    string creatureName = "Gary";
    [SerializeField]
    int health = 35;

    [SerializeField]
    GameObject creaturePrefab;


    // Start is called before the first frame update
    void Start()
    {
        creatureName = "Greg";
        Debug.Log(creatureName);

        //Instantiate(creaturePrefab);
    }

    private void Awake()
    {
        
    }

    private void OnMouseDown()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("My name is " + creatureName);
        ++health;

    }
}
