using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CreatureControl : Mob
{
	[Tooltip("How much time to stop trying to move.")]
	public float movementReduceTime = 1;
	
	[Tooltip("How close is too close.")]
	public float obstacleTooClose = 1;
	public float creatureTooClose = 1;

	public float obstacleRepulsionMagnitude = 1;
	public float creatureRepulsionMagnitude = 1;
	public float cohesionMagnitude = 1;
	public float alignmentMagnitude = 1;
	public float maxMagnitude = 3;

	List<Transform> neighbouringCreatures = new List<Transform>();
	List<Transform> obstacles = new List<Transform>();

    void Awake()
    {

    }

    void Update()
    {
		Vector3 totalForce = Vector3.zero;

		if (obstacles.Count > 0)
		{
			Vector3 obstacleRepulsionForce = getRepulsion(obstacles, obstacleTooClose);
			obstacleRepulsionForce *= obstacleRepulsionMagnitude;

			totalForce += obstacleRepulsionForce;

			Debug.DrawRay(transform.position, obstacleRepulsionForce, Color.yellow);
		}

		if (neighbouringCreatures.Count > 0)
		{
			Vector3 creatureRepulsionForce = getRepulsion(neighbouringCreatures, creatureTooClose);
			creatureRepulsionForce *= creatureRepulsionMagnitude;

			Vector3 averagePosition;
			Vector2 averageDirection;
			getAverages(out averagePosition, out averageDirection);

			Vector3 towardCenter = averagePosition - transform.position;
			towardCenter.Normalize();

			Vector3 towardCenterForce = towardCenter * cohesionMagnitude;

			Vector3 alignmentForce = (Vector3)averageDirection * alignmentMagnitude;

			totalForce += creatureRepulsionForce + towardCenterForce + alignmentForce;

			Debug.DrawRay(transform.position, creatureRepulsionForce, Color.red);
			Debug.DrawRay(transform.position, towardCenterForce, Color.green);
			Debug.DrawRay(transform.position, alignmentForce, Color.blue);
		}

		float magnitude = Mathf.Min(maxMagnitude, totalForce.magnitude);
		totalForce = totalForce.normalized * magnitude;

		Debug.DrawRay(transform.position, totalForce);

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

	Vector3 getRepulsion(List<Transform> repulsors, float tooClose)
	{
		Vector3 sumRepulsion = new Vector3();

		foreach (var repulsor in repulsors)
		{
			Vector3 awayVector = transform.position - repulsor.position;

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
			neighbouringCreatures.Add(creature.transform);
		}
		else
		{
			obstacles.Add(collider.transform);
		}
    }

	void OnTriggerExit2D(Collider2D collider)
	{
		var creature = collider.gameObject.GetComponent<CreatureControl>();
		if (creature != null)
		{
			neighbouringCreatures.Remove(creature.transform);
		}
		else
		{
			obstacles.Remove(collider.transform);
		}
	}
}

