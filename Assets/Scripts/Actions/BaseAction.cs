using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAction : MonoBehaviour
{
    protected Unit unit = null;
    protected bool isActive = false;

    protected virtual void Awake() {
        unit = GetComponent<Unit>();
    }
}
