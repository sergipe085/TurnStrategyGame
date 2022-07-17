using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAction : BaseAction
{
    [SerializeField] private int maxAttackDistance = 1;

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

        GridPosition unitGridPosition = unit.GetGridPosition();

        for (int x = -maxAttackDistance; x <= maxAttackDistance; x++) {
            for (int y = -maxAttackDistance; y <= maxAttackDistance; y++) {
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
}
