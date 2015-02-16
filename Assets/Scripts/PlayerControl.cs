using UnityEngine;
using System.Collections;

public class PlayerControl : Mob
{
    [Tooltip("Name for Horizontal input axis")]
    public string HorizontalInput = "Horizontal";
    [Tooltip("Name for Vertical input axis")]
    public string VerticalInput = "Vertical";
    

    void Awake()
    {
    }

    void Update()
    {
        // Retrieve axis information
        float inputX = Input.GetAxis(HorizontalInput);
        float inputY = Input.GetAxis(VerticalInput);

        UpdateMovement(inputX, inputY);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
    }
}

