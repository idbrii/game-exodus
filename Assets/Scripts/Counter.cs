using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Counter : MonoBehaviour {

    // public so we can see the count when debugging.
    [Tooltip("DO NOT EDIT: The number of items inside.")]
    public int count = 0;

    public int ContainedCount { get { return count; } }

    Text counterDisplay;

    void Start()
    {
        Dbg.Assert(count == 0, "You cannot edit Count.");

        // The counter must have an output.
        counterDisplay = GetComponentsInChildren<Text>()[0];
    }

    void Update()
    {
        counterDisplay.text = ""+ count;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        count += 1;
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        count -= 1;
    }
}
