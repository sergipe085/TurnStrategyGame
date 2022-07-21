using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GridSystemVisual;

public abstract class BaseAction : MonoBehaviour
{
    public static event EventHandler OnAnyActionStarted = null;
    public static event EventHandler OnAnyActionCompleted = null;

    [SerializeField] public GridType gridType = GridType.White;
    [SerializeField] private Material gridMaterial = null;
    [SerializeField] private int actionCost = 1;
    protected Unit unit = null;
    protected bool isActive = false;
    protected Action OnActionCompleteEvent;

    protected virtual void Awake() {
        unit = GetComponent<Unit>();
    }

    public abstract string GetActionName();

    public virtual void TakeAction(GridPosition gridPosition, Action onActionComplete) {}

    protected void ActionStart(Action onActionComplete) {
        isActive = true;
        OnActionCompleteEvent = onActionComplete;
        OnAnyActionStarted?.Invoke(this, null);
    }

    public virtual bool IsValidActionGridPosition(GridPosition gridPosition) {
        return GetValidActionGridPositionList().Contains(gridPosition);
    }

    public abstract List<GridPosition> GetValidActionGridPositionList();

    public virtual int GetActionPointsCost() {
        return actionCost;
    }

    protected void ActionComplete() {
        isActive = false;
        OnActionCompleteEvent?.Invoke();
        OnAnyActionCompleted?.Invoke(this, null);
    }

    public Unit GetUnit() {
        return unit;
    }

    public Material GetGridMaterial() {
        return gridMaterial;
    }
}
