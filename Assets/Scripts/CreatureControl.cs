using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CreatureControl : Mob
{
	[Tooltip("How much time to stop trying to move.")]
	public float movementReduceTime = 1;
	
	[Tooltip("How close is too close.")]
	public float tooClose = 1;

	public float repulsionMagnitude = 1;
	public float cohesionMagnitude = 1;
	public float alignmentMagnitude = 1;
	public float maxMagnitude = 3;
	
	private Vector2 direction;
    private Vector2 directionRate = Vector2.zero;
    private float urgency = 0;

	List<CreatureControl> neighbouringCreatures = new List<CreatureControl>();

    void Awake()
    {

    }

    void Update()
    {
		if (neighbouringCreatures.Count == 0)
		{
			return;
		}

		Vector3 repulsionForce = getRepulsion();
		repulsionForce *= repulsionMagnitude;

		Vector3 averagePosition;
		Vector2 averageDirection;
		getAverages(out averagePosition, out averageDirection);

		Vector3 towardCenter = averagePosition - transform.position;
		towardCenter.Normalize();

		Vector3 towardCenterForce = towardCenter * cohesionMagnitude;

		Vector3 alignmentForce = (Vector3)averageDirection * alignmentMagnitude;

		Vector3 totalForce = repulsionForce + towardCenterForce + alignmentForce;
		float magnitude = Mathf.Min(maxMagnitude, totalForce.magnitude);
		totalForce = totalForce.normalized * magnitude;

		Debug.DrawRay(transform.position, repulsionForce, Color.red);
		Debug.DrawRay(transform.position, towardCenterForce, Color.green);
		Debug.DrawRay(transform.position, alignmentForce, Color.blue);
		Debug.DrawRay(transform.position, totalForce);

		//rigidbody2D.AddForce((Vector2)totalForce);

		UpdateMovement(totalForce.x, totalForce.y);
    }

	void getAverages(out Vector3 averagePosition, out Vector2 averageDirection)
	{
		averagePosition = new Vector3();
		averageDirection = new Vector2();

		foreach (var creature in neighbouringCreatures)
		{
			averagePosition += creature.transform.position;
			averageDirection += creature.rigidbody2D.velocity.normalized;
		}

		averagePosition /= neighbouringCreatures.Count;
		averageDirection /= neighbouringCreatures.Count;
	}

	Vector3 getRepulsion()
	{
		Vector3 sumRepulsion = new Vector3();

		foreach (var creature in neighbouringCreatures)
		{
			Vector3 awayVector = transform.position - creature.transform.position;
			if (awayVector.magnitude < tooClose)
			{
				var scale = (tooClose - awayVector.magnitude) / tooClose;
				sumRepulsion += awayVector.normalized * scale;
			}
		}

        return sumRepulsion;
	}

    void OnTriggerEnter2D(Collider2D collider)
    {
        var creature = collider.gameObject.GetComponent<CreatureControl>();
		if (creature != null)
        {
			neighbouringCreatures.Add(creature);
		}
    }

	void OnTriggerExit2D(Collider2D collider)
	{
		var creature = collider.gameObject.GetComponent<CreatureControl>();
		if (creature != null)
		{
			neighbouringCreatures.Remove(creature);
		}
	}
}

