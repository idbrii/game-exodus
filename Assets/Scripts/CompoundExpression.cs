using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CompoundExpression : Expression {

    [Tooltip("The Counter for the first input.")]
    public Counter first;
    [Tooltip("The mathematical operation to perform.")]
    public MathOperator op;
    [Tooltip("The Counter for the second input.")]
    public Counter second;
    [Tooltip("The UI component to show our operator.")]
    public Text operatorDisplay;
    

    override public int ContainedValue
    {
        get
        {
            return op.compute(first.ContainedValue, second.ContainedValue);
        }
    }
    

    void Start()
    {
        operatorDisplay.text = op.GetLabel();
    }
}
