using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RandomInstanceStrategy : InstanceStrategy {
    [Tooltip("List of possible prefabs to spawn.")]
    public List<Transform> typesToSpawn;
    [Tooltip("A nonnegative value that increases the odds of spawning the same instance.")]
    public int encourageRepetitionFactor = 1;
    
    
    private Randomizer rand;
    private int lastIndex;

    void Awake() {
        rand = new Randomizer();

        Dbg.Assert(encourageRepetitionFactor >= 0, "encourageRepetitionFactor must be nonnegative (greater than or equal to zero!");
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

