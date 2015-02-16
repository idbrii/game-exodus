using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GroundSpawner : MonoBehaviour {
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
        delta -= spriteOverlap;

        int last_block = 0;
        var start_pos = new Vector3(width / 2.0f, height / 2.0f, 0);
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
		if (block > groundBlocks.Count)
        {
            block = last_block;
        }
        return 0;
    }
}
