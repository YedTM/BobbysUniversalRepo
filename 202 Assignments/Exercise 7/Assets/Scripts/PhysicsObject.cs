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
    bool frictionEnabled;

    [SerializeField]
    bool gravityEnabled;

    float coefficient = 0.8f;

    float gravityStrength = 9.81f;

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
        if (gravityEnabled)
        {
            ApplyGravity(Vector3.down * gravityStrength);
        }
        
        if (frictionEnabled)
        {
            ApplyFriction(coefficient);
        }

        velocity += acceleration * Time.deltaTime;
        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);

        //BounceOffEdges();

        position += velocity * Time.deltaTime;
        direction = velocity.normalized;
        RotateObject();

        transform.position = position;

        acceleration = Vector3.zero;
    }

    public void ApplyForce(Vector3 force)
    {
        acceleration += force / mass;
    }

    private void ApplyGravity(Vector3 gravity) 
    {
        acceleration += gravity;
    }

    private void BounceOffEdges()
    {
        if (position.x <= ScreenMin.x)
        {
            velocity.x *= -1;
            position.x = ScreenMin.x;
        }
        else if (position.x >= ScreenMax.x)
        {
            velocity.x *= -1;
            position.x = ScreenMax.x;
        }

        if (position.y <= ScreenMin.y)
        {
            velocity.y *= -1;
            position.y = ScreenMin.y;
        }
        else if (position.y >= ScreenMax.y)
        {
            velocity.y *= -1;
            position.y = ScreenMax.y;
        }
    }

    private void ApplyFriction(float coeff)
    {
        Vector3 friction = velocity * -1;
        friction.Normalize();
        friction = friction * coeff;
        ApplyForce(friction);
    }

    private void RotateObject()
    {
        transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
    }

    public void StopMoving()
    {
        velocity = Vector3.zero;
        acceleration = Vector3.zero;
    }

}
