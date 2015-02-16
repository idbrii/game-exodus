using UnityEngine;
using System.Collections;

public class EnemyControl : Mob
{
    [Tooltip("How much time to stop trying to move.")]
    public float movementReduceTime = 1;
    
    private Vector2 direction;
    private Vector2 directionRate = Vector2.zero;
    private float urgency = 0;

    void Awake()
    {

    }

    void Update()
    {
        var input = direction * urgency;
        UpdateMovement(input.x, input.y);

        direction = Vector2.SmoothDamp(direction, Vector2.zero, ref directionRate, movementReduceTime);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        var player = collider.gameObject.GetComponent<PlayerControl>();
        if (player != null)
        {
            direction = transform.position - player.transform.position;
            direction = Vector3.Normalize(direction);
            urgency = 1.0f;
        }
        else
        {
            direction = transform.position * -1.0f;
            direction = Vector3.Normalize(direction);
            urgency = 0.1f;
        }
    }
}

