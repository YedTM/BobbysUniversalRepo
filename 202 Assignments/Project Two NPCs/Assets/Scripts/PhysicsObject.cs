using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject : MonoBehaviour
{
    Vector3 position;
    Vector3 velocity;
    Vector3 direction;

    [SerializeField]
    Vector3 acceleration;

    [SerializeField]
    float mass = 1.0f;

    [SerializeField]
    float maxSpeed = 1.0f;

    [SerializeField]
    float radius;

    public Vector3 ScreenMax
    {
        get
        {
            return new Vector2(Camera.main.transform.position.x + Camera.main.orthographicSize * Camera.main.aspect,
                               Camera.main.transform.position.y + Camera.main.orthographicSize);
        }
    }

    public Vector3 ScreenMin
    {
        get
        {
            return new Vector2(Camera.main.transform.position.x - Camera.main.orthographicSize * Camera.main.aspect,
                               Camera.main.transform.position.y - Camera.main.orthographicSize);
        }
    }

    public float MaxSpeed
    {
        get
        {
            return maxSpeed;
        }
    }

    public Vector3 Velocity
    {
        get
        {
            return velocity;
        }
    }

    public float Radius
    {
        get
        {
            return radius;
        }
    }

    public Vector3 Position
    {
        get { return position; }
        set { position = value; }
    }
    // Start is called before the first frame update
    void Start()
    {
        position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        velocity += acceleration * Time.deltaTime;
        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);

        position += velocity * Time.deltaTime;
        direction = velocity.normalized;
        RotateObject();

        transform.position = position;

        acceleration = Vector3.zero;
    }

    /// <summary>
    /// Applied the force to the object
    /// </summary>
    /// <param name="force">The force being applied</param>
    public void ApplyForce(Vector3 force)
    {
        acceleration += force / mass;
    }

    private void RotateObject()
    {
        transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
    }
}
