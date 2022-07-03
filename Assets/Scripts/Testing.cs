using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    [SerializeField] private Unit unit;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.T)) {
            Unit selectedUnit = UnitActionSystem.Instance.GetSelectedUnit();
            List<GridPosition> validList = selectedUnit.GetMoveAction().GetValidActionGridPositionList();

            GridSystemVisual.Instance.HideAllGridPosition();
            GridSystemVisual.Instance.ShowGridPositionList(validList);
        }
    }

}
    
