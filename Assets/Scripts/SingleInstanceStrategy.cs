using UnityEngine;
using System.Collections;

public class SingleInstanceStrategy : InstanceStrategy {
    [Tooltip("The prefab to spawn.")]
    public Transform typeToSpawn;
    
    public override Transform NextInstanceType() {
        return typeToSpawn;
    }
}

