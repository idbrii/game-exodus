using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RandomInstanceStrategy : InstanceStrategy {
    [Tooltip("List of possible prefabs to spawn.")]
    public List<Transform> typesToSpawn;

    [Tooltip("Higher values increase the odds of spawning the same instance.")]
    [Range(0, 100)]
    public int encourageRepetitionFactor = 1;
    
    
    private Randomizer rand;
    private int lastIndex;

    void Awake() {
        rand = new Randomizer();
    }

    public override Transform NextInstanceType() {
        lastIndex = _GetRandomIndex(lastIndex);
        return typesToSpawn[lastIndex];
        
    }

    int _GetRandomIndex(int last_block) {
        int index = rand.Range(0, typesToSpawn.Count + encourageRepetitionFactor);
        if (index >= typesToSpawn.Count)
        {
            index = last_block;
        }
        return index;
    }
}

