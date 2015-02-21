using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GroundSpawner : MonoBehaviour {
    [Tooltip("Boundary blocker object.")]
    public Transform boundary;
    [Tooltip("Number of blocks high.")]
    public int height;
    [Tooltip("Number of blocks across.")]
    public int width;
    [Tooltip("The amount to overlap sprites.")]
    public Vector3 spriteOverlap;
    
    private InstanceStrategy typeToSpawn;
    
    
    void Awake() {
        typeToSpawn = gameObject.GetComponent<InstanceStrategy>();
    }

    void Start() {
        Transform default_block = typeToSpawn.NextInstanceType();
        var delta = default_block.renderer.bounds.size;
        delta.z = 0;
        delta -= spriteOverlap;

        Vector3 grid_dimensions = new Vector3(width, height, 0);
        var start_pos = grid_dimensions / 2.0f;
        start_pos *= -1.0f;
        start_pos = Vector3.Scale(start_pos, delta);

        var last_pos = start_pos;

        for (int y = 0; y < height; ++y) {
            for (int x = 0; x < width; ++x) {
                Transform block = typeToSpawn.NextInstanceType();
                last_pos.x += delta.x;
                _SpawnChild(block, last_pos);
            }

            last_pos.y += delta.y;
            last_pos.x = start_pos.x;
        }

        var total_bounds = new Bounds(Vector3.zero, Vector3.zero);
        var renderers = GetComponentsInChildren(typeof(Renderer));
        foreach (var component in renderers) {
            var render = (Renderer)component;
            total_bounds.Encapsulate(render.bounds);
        }

        var size = new Vector2(delta.x, total_bounds.size.y);
        var pos = total_bounds.min;
        pos.y += total_bounds.size.y / 2.0f;
        _SpawnBoundary(pos, size);
        pos.x += total_bounds.size.x;
        _SpawnBoundary(pos, size);

        size = new Vector2(total_bounds.size.x, delta.y);
        pos = total_bounds.min;
        pos.x += total_bounds.size.x / 2.0f;
        _SpawnBoundary(pos, size);
        pos.y += total_bounds.size.y;
        _SpawnBoundary(pos, size);
    }

    Transform _SpawnBoundary(Vector3 pos, Vector2 size) {
        var child = _SpawnChild(boundary, pos);
        var collider = child.GetComponent<BoxCollider2D>();
        collider.size = size;

        return child;
    }

    Transform _SpawnChild(Transform kind, Vector3 pos) {
        var child = (Transform)Instantiate(kind, pos, Quaternion.identity);
        child.parent = transform;
        return child;
    }

}
