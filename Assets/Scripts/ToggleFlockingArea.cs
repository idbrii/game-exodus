using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ToggleFlockingArea : MonoBehaviour {
    [Tooltip("Should we enable flocking when inside or outside the area (on enter or exit)?")]
    public bool setEnabledInsideArea = false;
    [Tooltip("The layer of flockers we want to toggle.")]
    public LayerMask targetLayer;
    

    // This list is only necessary so we can draw the contained flock. (Debug
    // only.)
    List<Transform> flock = new List<Transform>();

    void Update()
    {
        foreach (var f in flock)
        {
            var c = Color.yellow;
            Debug.DrawRay(f.position, Vector3.up, c);
            Debug.DrawRay(f.position, Vector3.down, c);
            Debug.DrawRay(f.position, Vector3.left, c);
            Debug.DrawRay(f.position, Vector3.right, c);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        var creature = GetFlocker(collider.transform);
        if (creature != null)
        {
            flock.Add(collider.transform);
            creature.SetFlocking(setEnabledInsideArea);
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        var creature = GetFlocker(collider.transform);
        if (creature != null)
        {
            creature.SetFlocking(!setEnabledInsideArea);
            flock.Remove(collider.transform);
        }
    }

    Flocking GetFlocker(Transform parent)
    {
        var flockers = parent.GetComponentsInChildren<Flocking>();
        foreach (var f in flockers)
        {
            if (LayerUtil.IsInMask(f.gameObject.layer, targetLayer))
            {
                return f;
            }
        }
        return null;
    }
}
