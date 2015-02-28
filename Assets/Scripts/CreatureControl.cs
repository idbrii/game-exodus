using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CreatureControl : MonoBehaviour
{
	[Tooltip("How close is too close.")]
	public float tooClose = 2;

    [Range(1,250)]
	public float repulsionMagnitude = 10;
    [Range(1,250)]
	public float cohesionMagnitude = 5;
    [Range(1,250)]
	public float alignmentMagnitude = 20;
    [Range(1,250)]
	public float maxMagnitude = 50;

	List<Transform> neighbouringCreatures = new List<Transform>();

	Vector3 totalForce = Vector3.zero;

    void Awake()
    {
    }

    void Update()
    {
		if (neighbouringCreatures.Count == 0)
		{
            totalForce = Vector3.zero;
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

		totalForce = repulsionForce + towardCenterForce + alignmentForce;
		float magnitude = Mathf.Min(maxMagnitude, totalForce.magnitude);
		totalForce = totalForce.normalized * magnitude;

		Debug.DrawRay(transform.position, repulsionForce, Color.red);
		Debug.DrawRay(transform.position, towardCenterForce, Color.green);
		Debug.DrawRay(transform.position, alignmentForce, Color.blue);
		Debug.DrawRay(transform.position, totalForce);
    }

	void FixedUpdate()
	{
		rigidbody2D.AddForce(totalForce);
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
		float sqrTooClose = Mathf.Pow(tooClose, 2.0f);

		Vector3 sumRepulsion = new Vector3();

		foreach (var repulsor in neighbouringCreatures)
		{
			Vector3 awayVector = transform.position - repulsor.position;

			if (awayVector.sqrMagnitude < sqrTooClose)
			{
				var scale = (sqrTooClose - awayVector.sqrMagnitude) / sqrTooClose;
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
    }

	void OnTriggerExit2D(Collider2D collider)
	{
		var creature = collider.gameObject.GetComponent<CreatureControl>();
		if (creature != null)
		{
			neighbouringCreatures.Remove(creature.transform);
		}
	}
}

