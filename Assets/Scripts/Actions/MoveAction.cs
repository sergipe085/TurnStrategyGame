using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAction : BaseAction
{
    [SerializeField] private MovementType movementType;
    [SerializeField] private int maxMoveDistance = 0;
    [SerializeField] private bool alignRotation = false;

    private Vector3 targetPosition = Vector3.zero;

    public event Action OnStartMoving;
    public event Action OnStopMoving;

    protected override void Awake() {
        base.Awake();
        targetPosition = transform.position;
    }

    private void Update() {
        if (!isActive) return;

        float stoppingDistance = 0.1f;
        Vector3 moveDirection = targetPosition - transform.position;
        if (Vector3.Distance(transform.position, targetPosition) > stoppingDistance) {
            moveDirection.Normalize();

            float moveSpeed = 4.0f;
            transform.position += moveDirection * Time.deltaTime * moveSpeed;
        }
        else {
            OnStopMoving?.Invoke();
            ActionComplete();
        }

        if (alignRotation) {
            transform.forward = Vector3.Lerp(transform.forward, moveDirection, 10.0f * Time.deltaTime);
        }
    }

    public override void TakeAction(GridPosition _targetGridPosition, Action onCompleteFunction) {
        base.TakeAction(_targetGridPosition, onCompleteFunction);

        this.targetPosition = LevelGrid.Instance.GetWorldPosition(_targetGridPosition);
        OnStartMoving?.Invoke();
        ActionStart(onCompleteFunction);
    }

    public override List<GridPosition> GetValidActionGridPositionList() {
        List<GridPosition> validGridPositionList = new List<GridPosition>();

        switch (movementType) {
            case MovementType.ALL:
                validGridPositionList.AddRange(GetDiagonalGridPositionList());
                validGridPositionList.AddRange(GetVHGridPositionList());
                break;
            case MovementType.DIAGONAL:
                validGridPositionList = GetDiagonalGridPositionList();
                break;
            case MovementType.VH:
                validGridPositionList = GetVHGridPositionList();
                break;
        }

        return validGridPositionList;
    }

    private List<GridPosition> GetDiagonalGridPositionList() {
        List<GridPosition> validGridPositionList = new List<GridPosition>();

        GridPosition unitGridPosition = unit.GetGridPosition();

        for (int x = -maxMoveDistance; x <= maxMoveDistance; x += 1) {
            for (int y = -maxMoveDistance; y <= maxMoveDistance; y += 1) {
                GridPosition offsetGridPosition = new GridPosition(x, y);
                GridPosition testGridPosition = unitGridPosition + offsetGridPosition;

                if (testGridPosition == unitGridPosition || 
                    !LevelGrid.Instance.IsGridPositionValid(testGridPosition) || 
                    LevelGrid.Instance.HasAnyUnitOnGridPosition(testGridPosition)) {
                    continue;
                }

                if (Mathf.Abs(x) == Mathf.Abs(y)) {
                    validGridPositionList.Add(testGridPosition);
                }
            }
        }

        return validGridPositionList;
    }

    private List<GridPosition> GetVHGridPositionList() {
        List<GridPosition> validGridPositionList = new List<GridPosition>();

        GridPosition unitGridPosition = unit.GetGridPosition();

        for (int x = -maxMoveDistance; x <= maxMoveDistance; x += 1) {
            for (int y = -maxMoveDistance; y <= maxMoveDistance; y += 1) {
                GridPosition offsetGridPosition = new GridPosition(x, y);
                GridPosition testGridPosition = unitGridPosition + offsetGridPosition;

                if (testGridPosition == unitGridPosition || 
                    !LevelGrid.Instance.IsGridPositionValid(testGridPosition) || 
                    (x != 0 && y != 0) ||
                    LevelGrid.Instance.HasAnyUnitOnGridPosition(testGridPosition)) {
                    continue;
                }

                validGridPositionList.Add(testGridPosition);
            }
        }

        return validGridPositionList;
    }

    public override string GetActionName() {
        return "MOVE";
    }
}

public enum MovementType { DIAGONAL, VH, ALL };