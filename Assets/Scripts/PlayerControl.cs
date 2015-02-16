using UnityEngine;
using System.Collections;

public class PlayerControl : Mob
{

    void Awake()
    {
    }

    void Update()
    {
        // Retrieve axis information
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        UpdateMovement(inputX, inputY);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
    }
}

