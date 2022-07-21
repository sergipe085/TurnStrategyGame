using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinAction : BaseAction
{
    private float totalSpinned = 0.0f;

    private void Update() {
        if (!isActive) return;

        float spinAddAmount = 360f * Time.deltaTime;
        transform.eulerAngles += new Vector3(0f, spinAddAmount, 0f);
        totalSpinned += spinAddAmount;

        if (totalSpinned >= 360f) {
            ActionComplete();
        }
    }

    public override void TakeAction(GridPosition gridPosition, Action onCompleteFunction) {
        base.TakeAction(gridPosition, onCompleteFunction);

        totalSpinned = 0.0f;
        
        ActionStart(onCompleteFunction);
    }

    public override string GetActionName() {
        return "SPIN";
    }

    public override List<GridPosition> GetValidActionGridPositionList() {
        List<GridPosition> validGridPositionList = new List<GridPosition>();
        validGridPositionList.Add(unit.GetGridPosition());
        return validGridPositionList;
    }
}
