using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObject
{
    private GridSystem gridSystem;
    private GridPosition gridPosition;
    private List<Unit> units = null;

    public GridObject(GridSystem _gridSystem, GridPosition _gridPosition) {
        this.gridSystem = _gridSystem;
        this.gridPosition = _gridPosition;
        units = new List<Unit>();
    }

    public override string ToString() {
        string unitString = "";
        foreach (Unit unit in units) {
            unitString += unit + "\n";
        }
        return gridPosition.ToString() + "\n" + unitString;
    }

    public void AddUnity(Unit _unit) {
        units.Add(_unit);
    }

    public void RemoveUnit(Unit _unit) {
        units.Remove(_unit);
    }

    public List<Unit> GetUnitList() {
        return units;
    }
}
