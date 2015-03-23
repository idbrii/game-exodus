using UnityEngine;

// This whole class is kind of dumb, but I don't have a way to make arbitrary
// classes selectable from the Unity editor, so instead I have this hack.
[System.Serializable]
public class MathOperator : System.Object {
    [Tooltip("The type of arithmetic we represent.")]
    public Operation kind;

    public enum Operation
    {
        ADD,
        SUBTRACT,
        MULTIPLY,
        DIVIDE
    }

    public int compute(int first, int second)
    {
        switch (kind)
        {
            case Operation.ADD:
                return first + second;

            case Operation.SUBTRACT:
                return first - second;

            case Operation.MULTIPLY:
                return first * second;

            case Operation.DIVIDE:
                return first / second;
        }

        Dbg.Assert(false, string.Format("Not all enum values handled. kind={0}", kind));
        return 0;
    }

    public string GetLabel()
    {
        switch (kind)
        {
            case Operation.ADD:
                return "+";

            case Operation.SUBTRACT:
                return "-";

            case Operation.MULTIPLY:
                return "*";

            case Operation.DIVIDE:
                return "/";
        }

        Dbg.Assert(false, string.Format("Not all enum values handled. kind={0}", kind));
        return "?";
    }
}
