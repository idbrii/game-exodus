using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GroundSpawner : MonoBehaviour {
    [Tooltip("Boundary blocker object.")]
    public Transform boundary;
    
    private GridPositionStrategy location;
    private InstanceStrategy typeToSpawn;
    
    
    void Awake() {
        location = gameObject.GetComponent<GridPositionStrategy>();
        typeToSpawn = gameObject.GetComponent<InstanceStrategy>();
    }

    void Start() {
        Transform default_block = typeToSpawn.NextInstanceType();
        location.Initialize(default_block);
        var delta = location.Delta;

        while (location.HasNext) {
            _SpawnChild(typeToSpawn.NextInstanceType(), location.NextPosition());
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
