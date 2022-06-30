using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObject
{
    private GridSystem gridSystem;
    private GridPosition gridPosition;

    private Unit unit = null;

    public GridObject(GridSystem _gridSystem, GridPosition _gridPosition) {
        this.gridSystem = _gridSystem;
        this.gridPosition = _gridPosition;
    }

    public override string ToString() {
        return gridPosition.ToString() + "\n" + unit;
    }

    public void SetUnit(Unit _unit) {
        unit = _unit;
    }

    public Unit GetUnit() {
        return unit;
    }

    public void ClearUnit() {
        unit = null;
    }
}
