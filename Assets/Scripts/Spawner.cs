using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

    [Tooltip("How many to spawn.")]
    public int numToSpawn = 5;
    [Tooltip("Set this object as the parent of new objects.")]
    public Transform desiredParent;

    protected PositionStrategy location;
    protected InstanceStrategy typeToSpawn;
    
    
    void Awake() {
        location = gameObject.GetComponent<PositionStrategy>();
        typeToSpawn = gameObject.GetComponent<InstanceStrategy>();
    }


    void Start() {
        SpawnObjects(new Vector3(1.0f,1.0f,1.0f));
    }

    protected void SpawnObjects(Vector3 pos_scale) {
        Quaternion rot = transform.rotation;

        for (int i = 0; i < numToSpawn; ++i) {
            Vector3 pos = Vector3.Scale(pos_scale, location.NextPosition());
            Transform obj = (Transform) Instantiate(typeToSpawn.NextInstanceType(), pos, rot);
            obj.parent = desiredParent;
        }
    }

}
