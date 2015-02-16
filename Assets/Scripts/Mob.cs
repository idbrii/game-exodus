using UnityEngine;
using System.Collections;

public class Mob : MonoBehaviour
{
    [Tooltip("The speed of the actor")]
    public Vector2 speed = new Vector2(10, 10);
    
    private Vector2 movement;


    protected void UpdateMovement(float inputX, float inputY)
    {
        movement = new Vector2(
                speed.x * inputX,
                speed.y * inputY);
    }

    void FixedUpdate()
    {
        rigidbody2D.velocity = movement;
    }
}

