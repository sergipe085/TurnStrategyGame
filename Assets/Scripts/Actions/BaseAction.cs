using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAction : MonoBehaviour
{
    [SerializeField] private int actionCost = 1;
    protected Unit unit = null;
    protected bool isActive = false;
    protected Action OnActionCompleteEvent;

    protected virtual void Awake() {
        unit = GetComponent<Unit>();
    }

    public abstract string GetActionName();

    public virtual void TakeAction(GridPosition gridPosition, Action onActionComplete) {
        isActive = true;
        OnActionCompleteEvent = onActionComplete;
    }

    public virtual bool IsValidActionGridPosition(GridPosition gridPosition) {
        return GetValidActionGridPositionList().Contains(gridPosition);
    }

    public abstract List<GridPosition> GetValidActionGridPositionList();

    public virtual int GetActionPointsCost() {
        return actionCost;
    }
}
