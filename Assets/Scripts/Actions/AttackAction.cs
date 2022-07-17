using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAction : BaseAction
{
    [SerializeField] private int maxAttackDistance = 1;
    [SerializeField] private AttackType attackType;

    private float totalSpinned = 0.0f;

    private void Update() {
        if (!isActive) return;

        float spinAddAmount = 360f * Time.deltaTime;
        transform.eulerAngles += new Vector3(0f, spinAddAmount, 0f);
        totalSpinned += spinAddAmount;

        if (totalSpinned >= 360f) {
            isActive = false;
            OnActionCompleteEvent?.Invoke();
        }
    }

    public override void TakeAction(GridPosition gridPosition, Action onCompleteFunction) {
        base.TakeAction(gridPosition, onCompleteFunction);
        totalSpinned = 0.0f;
    }

    public override string GetActionName() {
        return "ATTACK";
    }

    public override List<GridPosition> GetValidActionGridPositionList() {
        List<GridPosition> validGridPositionList = new List<GridPosition>();

        switch (attackType) {
            case AttackType.ALL:
                validGridPositionList.AddRange(GetDiagonalGridPositionList());
                validGridPositionList.AddRange(GetVHGridPositionList());
                break;
            case AttackType.DIAGONAL:
                validGridPositionList = GetDiagonalGridPositionList();
                break;
            case AttackType.VH:
                validGridPositionList = GetVHGridPositionList();
                break;
        }

        return validGridPositionList;
    }

    private List<GridPosition> GetDiagonalGridPositionList() {
        List<GridPosition> validGridPositionList = new List<GridPosition>();

        GridPosition unitGridPosition = unit.GetGridPosition();

        for (int x = -maxAttackDistance; x <= maxAttackDistance; x += 2) {
            for (int y = -maxAttackDistance; y <= maxAttackDistance; y += 2) {
                GridPosition offsetGridPosition = new GridPosition(x, y);
                GridPosition testGridPosition = unitGridPosition + offsetGridPosition;

                if (testGridPosition == unitGridPosition || !LevelGrid.Instance.IsGridPositionValid(testGridPosition)) {
                    continue;
                }

                Unit testUnit = LevelGrid.Instance.GetUnitAtGridPosition(testGridPosition);

                if (testUnit && testUnit.IsEnemy() != unit.IsEnemy()) {
                    validGridPositionList.Add(testGridPosition);
                }
            }
        }

        return validGridPositionList;
    }

    private List<GridPosition> GetVHGridPositionList() {
        List<GridPosition> validGridPositionList = new List<GridPosition>();

        GridPosition unitGridPosition = unit.GetGridPosition();

        for (int x = -maxAttackDistance; x <= maxAttackDistance; x += 1) {
            for (int y = -maxAttackDistance; y <= maxAttackDistance; y += 1) {
                GridPosition offsetGridPosition = new GridPosition(x, y);
                GridPosition testGridPosition = unitGridPosition + offsetGridPosition;

                if (testGridPosition == unitGridPosition || !LevelGrid.Instance.IsGridPositionValid(testGridPosition) || (x != 0 && y != 0)) {
                    continue;
                }

                Unit testUnit = LevelGrid.Instance.GetUnitAtGridPosition(testGridPosition);

                if (testUnit && testUnit.IsEnemy() != unit.IsEnemy()) {
                    validGridPositionList.Add(testGridPosition);
                }
            }
        }

        return validGridPositionList;
    }
}

public enum AttackType { DIAGONAL, VH, ALL };
