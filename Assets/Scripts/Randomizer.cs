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

    public float Range(float min, float max) {
        if (max == min)
        {
            return min;
        }

        const float RAND_MAX = 1.0f;
        float scale = RAND_MAX / (float)(max - min);
        float r = min + (Random.value / scale);
        return r;
    }

    public int Range(int min, int max) {
        float min_f = min;
        float max_f = max;
        float r = Range(min_f, max_f);
        return (int)Mathf.Round(r);
    }

    public float PositiveNumber(float max) {
        const float zero = 0;
        return Range(zero, max);
    }

    public int PositiveNumber(int max) {
        const int zero = 0;
        return Range(zero, max);
    }

    public Vector3 UnitVectorFlat() {
        const float max = 5.0f;
        Vector3 v = new Vector3(Range(-max,max), Range(-max,max));
        v.Normalize();
        return v;
    }

    public Vector3 UnitVector() {
        const float max = 5.0f;
        Vector3 v = new Vector3(Range(-max,max), Range(-max,max), Range(-max,max));
        v.Normalize();
        return v;
    }
}
