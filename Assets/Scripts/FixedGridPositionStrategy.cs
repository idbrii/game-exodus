using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FixedGridPositionStrategy : PositionStrategy {
    [Tooltip("Number of blocks high.")]
    public int height = 10;
    [Tooltip("Number of blocks across.")]
    public int width = 10;

    private IEnumerator<Vector3> iter;
    private bool hasNext = false;


    void Awake() {
        iter = CreateIterator();
        // To have a valid "hasNext" value, we need to call MoveNext after
        // creating the iter.
        MoveNext();
    }

    public override bool HasNext() {
        return hasNext;
    }

    public override Vector3 NextPosition() {
        var v = iter.Current;
        MoveNext();
        return v;
    }

    void MoveNext() {
        hasNext = iter.MoveNext();
    }

    IEnumerator<Vector3> CreateIterator() {
        // Try to centre our grid.
        Vector3 grid_dimensions = new Vector3(width, height, 0);
        var start_pos = grid_dimensions / 2.0f;
        start_pos *= -1.0f;

        var last_pos = start_pos;

        for (int y = 0; y < height; ++y) {
            for (int x = 0; x < width; ++x) {
                ++last_pos.x;
                yield return last_pos;
            }

            ++last_pos.y;
            last_pos.x = start_pos.x;
        }
    }
}
