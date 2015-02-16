using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GroundSpawner : MonoBehaviour {
    [Tooltip("Boundary blocker object.")]
    public Transform boundary;
    [Tooltip("List of possible ground tiles.")]
    public List<Transform> groundBlocks;
    [Tooltip("Number of blocks high.")]
    public int height;
    [Tooltip("Number of blocks across.")]
    public int width;
    [Tooltip("The amount to overlap sprites.")]
    public Vector3 spriteOverlap;
    
    
    
	// Use this for initialization
    void Start() {
        Random.seed = 0;

        Transform default_block = groundBlocks[0];
        var delta = default_block.renderer.bounds.size;
        delta.z = 0;
        delta -= spriteOverlap;

        Vector3 grid_dimensions = new Vector3(width, height, 0);
        int last_block = 0;
        var start_pos = grid_dimensions / 2.0f;
        start_pos *= -1.0f;
        start_pos = Vector3.Scale(start_pos, delta);

        var last_pos = start_pos;

        for (int y = 0; y < height; ++y) {
            for (int x = 0; x < width; ++x) {
                last_block = _GetRandomBlock(last_block);

                Transform block = groundBlocks[last_block];
                last_pos.x += delta.x;
                _SpawnChild(block, last_pos);
            }

            last_pos.y += delta.y;
            last_pos.x = start_pos.x;
        }

        start_pos.z = -1.0f;

        var extent = grid_dimensions;
        extent.x -= 1;
        extent.y -= 1;
        extent /= 2.0f;
        extent = Vector3.Scale(delta, extent);
        var half_extent = extent / 4.0f;
        extent.z = -1.0f;
        var pos = extent;
        pos.x = half_extent.x;
        pos.y += delta.y * 1.5f;
        _SpawnChild(boundary, pos);
        pos = extent;
        pos.x += delta.x * 1.5f;
        pos.y = half_extent.y;
        _SpawnChild(boundary, pos);
        pos = start_pos;
        pos.y = half_extent.y;
        _SpawnChild(boundary, pos);
        pos = start_pos;
        pos.x = half_extent.x;
        pos.y -= delta.y * 1.5f;
        _SpawnChild(boundary, pos);
    }

    Transform _SpawnChild(Transform kind, Vector3 pos) {
        var b = (Transform)Instantiate(kind, pos, Quaternion.identity);
        b.parent = transform;
        return b;
    }

    int _Random(int min, int max) {
        if (max == min)
        {
            return min;
        }

        const float RAND_MAX = 1.0f;
        float scale = RAND_MAX / (float)(max - min);
        float r = min + (Random.value / scale);
        return (int)Mathf.Round(r);
    }

    int _GetRandomBlock(int last_block) {
        int block = _Random(0, groundBlocks.Count + 1);
		if (block >= groundBlocks.Count)
        {
            block = last_block;
        }
        return block;
    }
}
