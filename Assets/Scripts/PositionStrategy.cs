using UnityEngine;
using System.Collections;

public abstract class PositionStrategy : MonoBehaviour {
    public abstract Vector3 NextPosition();
    public abstract bool HasNext();
}

