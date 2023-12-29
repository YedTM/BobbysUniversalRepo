using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SceneManager : MonoBehaviour
{
    [SerializeField]
    List<PhysicsObject> physicsObjects;
    Vector2 mousePosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        physicsObjects[0].ApplyForce(mousePosition - (Vector2)physicsObjects[0].transform.position * 5);
        physicsObjects[1].ApplyForce(mousePosition - (Vector2)physicsObjects[1].transform.position * 3);
        physicsObjects[2].ApplyForce(mousePosition - (Vector2)physicsObjects[2].transform.position);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(Vector3.zero, mousePosition);
    }
}
