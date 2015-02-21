using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GridPositionStrategy : PositionStrategy {
    [Tooltip("Number of blocks high.")]
    public int height;
    [Tooltip("Number of blocks across.")]
    public int width;
    [Tooltip("The amount to overlap sprites.")]
    public Vector3 spriteOverlap;
    
    private IEnumerator<Vector3> iter;
    private Vector3 delta;
    public Vector3 Delta {
        get {
            return this.delta;
        }
    }
    public bool HasNext { get; private set; }
    
    void Awake() {
    }

    public void Initialize(Transform sample_instance) {
        delta = sample_instance.renderer.bounds.size;
        delta.z = 0;
        delta -= spriteOverlap;

        iter = CreateIter();
        HasNext = iter.MoveNext();
    }

    public override Vector3 NextPosition() {
        var v = iter.Current;
        HasNext = iter.MoveNext();
        return v;
    }

    IEnumerator<Vector3> CreateIter() {
        Vector3 grid_dimensions = new Vector3(width, height, 0);
        var start_pos = grid_dimensions / 2.0f;
        start_pos *= -1.0f;
        start_pos = Vector3.Scale(start_pos, delta);

        var last_pos = start_pos;

        for (int y = 0; y < height; ++y) {
            for (int x = 0; x < width; ++x) {
                last_pos.x += delta.x;
                yield return last_pos;
            }

            last_pos.y += delta.y;
            last_pos.x = start_pos.x;
        }
    }
}
