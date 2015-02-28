using UnityEngine;
using System.Collections;

public class RandomPositionStrategy : PositionStrategy {
    [Tooltip("The root of where we are spawning from.")]
    public Transform rootObject;
    [Tooltip("How big of a circular area to spawn.")]
    public float spawnRadius = 5.0f;
    [Tooltip("Instead of random spacing, force to spawnRadius from Spawner.")]
    public bool forceToMaxDistance = false;
    
    private Randomizer rand;

    void Awake() {
        rand = new Randomizer();
    }

	public override bool HasNext() {
		// We can randomly place forever.
		return true;
	}

    public override Vector3 NextPosition() {
        return rootObject.transform.position + GetSpawnOffset();
    }

    float GetSpawnDistance() {
        if (forceToMaxDistance) {
            return spawnRadius;
        }
        else {
            return rand.PositiveNumber(spawnRadius);
        }
    }

    Vector3 GetSpawnOffset() {
        Vector3 offset_direction = rand.UnitVectorFlat();
        offset_direction *= GetSpawnDistance();
        return offset_direction;
    }
}

