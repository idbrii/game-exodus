using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GroundSpawner : Spawner {
    [Tooltip("Boundary blocker object.")]
    public Transform boundary;
    [Tooltip("The amount to overlap sprites.")]
    public Vector3 spriteOverlap;
    
    
    void Start() {
        Transform sample_instance = typeToSpawn.NextInstanceType();
        var delta = sample_instance.renderer.bounds.size;
        delta.z = 0;
        delta -= spriteOverlap;
        base.SpawnObjects(delta);

        if (boundary != null) {
            var total_bounds = new Bounds(transform.position, transform.position);
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
