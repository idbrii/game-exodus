using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

    [Tooltip("The prefab to spawn.")]
    public Transform typeToSpawn;
    [Tooltip("How many to spawn.")]
    public int numToSpawn = 5;
    [Tooltip("How big of a circular area to spawn.")]
    public float spawnRadius = 5.0f;
    [Tooltip("Set this object as the parent of new objects.")]
    public bool shouldParentNewObjectsToMe = true;
    [Tooltip("Instead of random spacing, force to spawnRadius from Spawner.")]
    public bool forceToMaxDistance = false;
    
    
    
    private Randomizer rand;
    
    
    void Awake() {
        rand = new Randomizer();
    }


	void Start() {
        SpawnObjects();
    }

    void SpawnObjects() {
        Quaternion rot = transform.rotation;

        for (int i = 0; i < numToSpawn; ++i) {
            Vector3 offset = GetSpawnOffset();
            Vector3 pos = transform.position + offset;
            Transform obj = (Transform) Instantiate(typeToSpawn, pos, rot);
            if (shouldParentNewObjectsToMe) {
                obj.parent = transform;
            }
        }
	
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
