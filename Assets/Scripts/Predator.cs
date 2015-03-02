using UnityEngine;
using System.Collections;

public class Predator : MonoBehaviour
{
    [Tooltip("Minimum distance to attack prey.")]
    public float minAttackDistance = 1.5f;
    // TODO: Figure out if I'm at a boundary instead.
    public float minDistanceFromCentreToEat = 25.0f;
    [Tooltip("Magnitude of run force.")]
    public float runMagnitude = 50.0f;
    
    [Tooltip("Flocking for attacking prey.")]
    public Flocking preyVision;
    [Tooltip("Flocking for fleeing enemies.")]
    public Flocking enemyVision;
    [Tooltip("The area that we're hunting within.")]
    public Transform huntingGrounds;

    // Using a Collider2D to make sure it's something that can fire
    // OnCollisionExit2D.
    [Tooltip("Collision where it's safe to eat.")]
    public Collider2D safeToEatAreaCollision;
    
    
    // Only public to allow viewState to be seen in editor.
    public enum State
    {
        HUNGRY,
        GOT_FOOD,
        EATING,
    }

    [Tooltip("DO NOT EDIT. For debug editor viewing only.")]
    public State viewState;

    private State currentState = State.HUNGRY;

    Vector3 totalForce = Vector3.zero;

    bool isNearBorder = false;

    Transform capturedPrey;

    DistanceJoint2D joint;


	void Start()
    {
        joint = GetComponent<DistanceJoint2D>();

        // To start, we only track prey and don't attack.
        SwitchTo(State.HUNGRY);
	}
	
	void Update()
    {
        switch (currentState)
        {
            case State.HUNGRY:
                UpdateHungry();
                break;

            case State.GOT_FOOD:
                UpdateGotFood();
                break;

            case State.EATING:
                UpdateEating();
                break;
        }

        viewState = currentState;
    }

    void SwitchTo(State new_state)
    {
        currentState = new_state;

        preyVision.SetFlocking(false);
        totalForce = Vector3.zero;
    }

    void UpdateHungry()
    {
        // we only hunt if there is a lone target
        bool is_hunting = preyVision.Neighbours.Count == 1;
        preyVision.SetFlocking(is_hunting);

        if (is_hunting)
        {
            // Since we're destroying objects we eat, we may find timing issues
            // between when the object is added to Neighbours and when it's
            // destroyed. Check to ensure it exists.
            // TODO: Is there a better way to define our ordering so this
            // doesn't occur? Is it possible for us to capture the object here
            // and then lose it next frame?
            capturedPrey = preyVision.Neighbours[0];
            if (capturedPrey != null)
            {
                Vector3 to_prey = capturedPrey.position - transform.position;
                float sqr_distance_to_prey = to_prey.sqrMagnitude;
                if (sqr_distance_to_prey < Mathf.Pow(minAttackDistance, 2.0f))
                {
                    // Attach it to me.
                    joint.connectedBody = capturedPrey.rigidbody2D;
                    joint.enabled = true;

                    foreach (var c in capturedPrey.GetComponentsInChildren<Flocking>())
                    {
                        c.SetFlocking(false);
                    }

                    SwitchTo(State.GOT_FOOD);
                }
            }
        }
        else
        {
            // TODO: Pace back and forth.
            //totalForce = ;
        }
	}

    void UpdateGotFood()
    {
        Vector3 away_from_centre = transform.position - huntingGrounds.transform.position;
        if (isNearBorder)
        {
            SwitchTo(State.EATING);
        }
        else
        {
            away_from_centre.Normalize();
            totalForce = away_from_centre * runMagnitude;
        }
    }

    void UpdateEating()
    {
        // TODO: Defer this for a bit.
        Object.Destroy(capturedPrey.gameObject);
        capturedPrey = null;
        joint.enabled = false;
        joint.connectedBody = null;

        SwitchTo(State.HUNGRY);
    }

    void FixedUpdate()
    {
        rigidbody2D.AddForce(totalForce);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(safeToEatAreaCollision.gameObject.tag))
        {
            isNearBorder = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(safeToEatAreaCollision.gameObject.tag))
        {
            isNearBorder = false;
        }
    }
}
