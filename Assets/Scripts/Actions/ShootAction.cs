using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAction : BaseAction
{
    [SerializeField] private int maxShootDistance = 1;

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
        return "SHOOT";
    }

    public override List<GridPosition> GetValidActionGridPositionList() {
        List<GridPosition> validGridPositionList = new List<GridPosition>();

        GridPosition unitGridPosition = unit.GetGridPosition();

        for (int x = -maxShootDistance; x <= maxShootDistance; x++) {
            for (int z = -maxShootDistance; z <= maxShootDistance; z++) {
                GridPosition offsetGridPosition = new GridPosition(x, z);
                GridPosition testGridPosition = unitGridPosition + offsetGridPosition;

                if (!LevelGrid.Instance.IsGridPositionValid(testGridPosition)) {
                    continue;
                }

                int testDistance = Mathf.Abs(x) + Mathf.Abs(z);
                if (testDistance > maxShootDistance) {
                    continue;
                }

                if (testGridPosition == unitGridPosition) {
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
