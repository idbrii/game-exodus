using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public abstract class Expression : MonoBehaviour {

    public abstract int ContainedValue
    {
        get;
    }
}
