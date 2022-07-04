using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class UnitActionSystem : MonoBehaviour
{
    public static UnitActionSystem Instance { get; private set; }

    public event EventHandler OnSelectedUnitChange;

    [SerializeField] private Unit selectedUnit = null;
    [SerializeField] private LayerMask unitLayerMask;

    private void Awake() {
        if (Instance != null) {
            Debug.LogError("There is more than one UnitActionSystem!" + transform + " - " + Instance);
            Destroy(this.gameObject);
            return;
        }
        
        Instance = this;
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            if (!TryHandleUnitSelection())
                MoveSelectedUnit();
        }

        if (Input.GetMouseButtonDown(1)) {
            selectedUnit.GetSpinAction().Spin();
        }
    }

    private bool TryHandleUnitSelection() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, unitLayerMask)) {
            if (hit.transform.TryGetComponent<Unit>(out Unit unit)) {
                SelectUnit(unit);
                return true;
            }
        }

        return false;
    }

    private void SelectUnit(Unit unit) {
        selectedUnit = unit;
        OnSelectedUnitChange?.Invoke(this, EventArgs.Empty);
    }

    private void MoveSelectedUnit() {
        if (!selectedUnit) return;

        Vector3 targetPosition = MouseWorld.GetPosition();
        GridPosition targetGridPosition = LevelGrid.Instance.GetGridPosition(targetPosition);

        if (selectedUnit.GetMoveAction().IsValidActionGridPosition(targetGridPosition)) {
            selectedUnit.GetMoveAction().Move(targetGridPosition);
        }
    }

    public Unit TryGetUnit() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, unitLayerMask)) {
            if (hit.transform.TryGetComponent<Unit>(out Unit unit)) {
                return unit;
            }
        }
        
        return null;
    }

    public Unit GetSelectedUnit() {
        return selectedUnit;
    }
}
