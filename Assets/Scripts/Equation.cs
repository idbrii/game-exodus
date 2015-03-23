using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Equation : MonoBehaviour {

    [Tooltip("The Expression for the left-hand side.")]
    public Expression lhs;
    [Tooltip("The Expression for the right-hand side.")]
    public Expression rhs;


    public bool IsEqual
    {
        get
        {
            return lhs.ContainedValue == rhs.ContainedValue;
        }
    }
    

    void Start()
    {
    }

    void Update()
    {
        if (IsEqual)
        {
            // The expression must have an output for the operator.
            foreach (var t in GetComponentsInChildren<Text>())
            {
                t.text = "Woo hoo!";
            }
        }
    }
}
