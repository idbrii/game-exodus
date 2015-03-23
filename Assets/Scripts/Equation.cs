using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Equation : MonoBehaviour {

    [Tooltip("The Expression for the left-hand side.")]
    public Expression lhs;
    [Tooltip("The Expression for the right-hand side.")]
    public Expression rhs;
    [Tooltip("Where the objects we're counting come from.")]
    public Spawner source;


    bool IsEqual()
    {
        return lhs.ContainedValue == rhs.ContainedValue;
    }

    bool IsUsingAllCreatures()
    {
        return (lhs.ContainedValue * 2) == source.numToSpawn;
    }
    
    bool IsSatisfied()
    {
        return IsEqual() && IsUsingAllCreatures();
    }
    
    void Update()
    {
        if (IsSatisfied())
        {
            // The expression must have an output for the operator.
            foreach (var t in GetComponentsInChildren<Text>())
            {
                t.text = "Woo hoo!";
            }
        }
    }
}
