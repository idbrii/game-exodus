using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

    [Tooltip("How many to spawn.")]
    public int numToSpawn = 5;
    [Tooltip("Set this object as the parent of new objects.")]
    public Transform desiredParent;

    private PositionStrategy location;
    private InstanceStrategy typeToSpawn;
    
    
    void Awake() {
        location = gameObject.GetComponent<PositionStrategy>();
        typeToSpawn = gameObject.GetComponent<InstanceStrategy>();
    }


    void Start() {
        SpawnObjects();
    }

    void SpawnObjects() {
        Quaternion rot = transform.rotation;

        for (int i = 0; i < numToSpawn; ++i) {
            Transform obj = (Transform) Instantiate(typeToSpawn.NextInstanceType(), location.NextPosition(), rot);
            obj.parent = desiredParent;
        }
    }

}
