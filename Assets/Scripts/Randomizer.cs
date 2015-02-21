using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Randomizer {
    static bool hasSetSeed = false;

    public Randomizer() {
        if (!hasSetSeed)
        {
            hasSetSeed = true;
            Random.seed = 0;
        }
    }

    public int Range(int min, int max) {
        if (max == min)
        {
            return min;
        }

        const float RAND_MAX = 1.0f;
        float scale = RAND_MAX / (float)(max - min);
        float r = min + (Random.value / scale);
        return (int)Mathf.Round(r);
    }
}
