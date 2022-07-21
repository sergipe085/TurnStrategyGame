using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpAction : BaseAction
{
    [SerializeField] private int maxJumpDistance = 4;
    [SerializeField] private Transform model = null;
    [SerializeField] private AnimationCurve jumpAnimationCurve = null;
    private Vector3 targetPosition;

    [SerializeField] private float jumpTime = 2.0f;
    private float currentJumpTime = 0.0f;

    private Vector3 initialPosition = Vector3.zero;

    private void Update() {
        if (!isActive) return;

        currentJumpTime += Time.deltaTime;
        float jumpOffset = currentJumpTime / jumpTime;

        transform.position = Vector3.Lerp(initialPosition, targetPosition, jumpOffset);
        model.localPosition = new Vector3(0f, jumpAnimationCurve.Evaluate(1 - jumpOffset) * maxJumpDistance);

        if (currentJumpTime >= jumpTime) {
            ActionComplete();
        }
    }

    public override void TakeAction(GridPosition gridPosition, Action onActionComplete) {
        base.TakeAction(gridPosition, onActionComplete);


        targetPosition = LevelGrid.Instance.GetWorldPosition(gridPosition);
        initialPosition = transform.position;
        currentJumpTime = 0.0f;

        transform.rotation = Quaternion.LookRotation(targetPosition - transform.position);
        ActionStart(onActionComplete);
    }

    public override List<GridPosition> GetValidActionGridPositionList() {
        List<GridPosition> validGridPositions = new List<GridPosition>();
        GridPosition unitGridPosition = unit.GetGridPosition();

        for (int x = -maxJumpDistance; x <= maxJumpDistance; x++) {
            for (int z = -maxJumpDistance; z <= maxJumpDistance; z++) {
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
        return "JUMP";
    }
}
