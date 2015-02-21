using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

    [Tooltip("The prefab to spawn.")]
    public Transform typeToSpawn;
    [Tooltip("How many to spawn.")]
    public int numToSpawn = 5;
    [Tooltip("Set this object as the parent of new objects.")]
    public Transform desiredParent;

    private PositionStrategy location;
    
    
    void Awake() {
        location = gameObject.GetComponent<PositionStrategy>();
    }


    void Start() {
        SpawnObjects();
    }

    void SpawnObjects() {
        Quaternion rot = transform.rotation;

        for (int i = 0; i < numToSpawn; ++i) {
            Vector3 pos = location.NextPosition();
            Transform obj = (Transform) Instantiate(typeToSpawn, pos, rot);
            obj.parent = desiredParent;
        }
    }

}
