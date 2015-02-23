using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GridPositionStrategy : PositionStrategy {
    [Tooltip("Number of blocks high.")]
    public int height = 10;
    [Tooltip("Number of blocks across.")]
    public int width = 10;
    
    private IEnumerator<Vector3> iter;

    
    void Awake() {
        iter = CreateIterator();
        // If we wanted to properly expose a "HasNext" value, we'd need to call
        // MoveNext after creating the iter.
    }

    public override Vector3 NextPosition() {
        iter.MoveNext();
        return iter.Current;
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
