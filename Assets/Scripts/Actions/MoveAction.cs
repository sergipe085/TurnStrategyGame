using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAction : BaseAction
{
    [SerializeField] private Animator unitAnimator = null;
    [SerializeField] private int maxMoveDistance = 0;

    private readonly int IsWalkingHash = Animator.StringToHash("IsWalking");

    private Vector3 targetPosition = Vector3.zero;

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

            unitAnimator.SetBool(IsWalkingHash, true);
        }
        else {
            unitAnimator.SetBool(IsWalkingHash, false);
            isActive = false;
            OnActionCompleteEvent?.Invoke();
        }

        float rotateSpeed = 20.0f;
        transform.forward = Vector3.Lerp(transform.forward, moveDirection, Time.deltaTime * rotateSpeed);
    }

    public override void TakeAction(GridPosition _targetGridPosition, Action onCompleteFunction) {
        base.TakeAction(_targetGridPosition, onCompleteFunction);
        this.targetPosition = LevelGrid.Instance.GetWorldPosition(_targetGridPosition);
    }

    public override List<GridPosition> GetValidActionGridPositionList() {
        List<GridPosition> validGridPositions = new List<GridPosition>();
        GridPosition unitGridPosition = unit.GetGridPosition();

        for (int x = -maxMoveDistance; x <= maxMoveDistance; x++) {
            for (int z = -maxMoveDistance; z <= maxMoveDistance; z++) {
                GridPosition offsetGridPosition = new GridPosition(x, z);
                GridPosition testGridPosition = unitGridPosition + offsetGridPosition;

                if (testGridPosition != unitGridPosition &&
                    LevelGrid.Instance.IsGridPositionValid(testGridPosition) &&
                    !LevelGrid.Instance.HasAnyUnitOnGridPosition(testGridPosition)
                ) {
                    validGridPositions.Add(testGridPosition);
                }
            }   
        }

        return validGridPositions;
    }

    public override string GetActionName() {
        return "MOVE";
    }

}
