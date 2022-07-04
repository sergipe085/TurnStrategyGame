using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnitActionSystem : MonoBehaviour
{
    public static UnitActionSystem Instance { get; private set; }

    public event EventHandler OnSelectedUnitChange;

    [SerializeField] private Unit selectedUnit = null;
    [SerializeField] private LayerMask unitLayerMask;

    private BaseAction selectedAction = null;
    private bool isBusy = false;

    private void Awake() {
        if (Instance != null) {
            Debug.LogError("There is more than one UnitActionSystem!" + transform + " - " + Instance);
            Destroy(this.gameObject);
            return;
        }
        
        Instance = this;
    }

    private void Start() {
        SetSelectedUnit(selectedUnit);
    }

    private void Update() {
        
        if (isBusy) return;

        if (EventSystem.current.IsPointerOverGameObject()) return;

        if (TryHandleUnitSelection()) return;

        HandleSelectedAction();
    }

    private void HandleSelectedAction() {
        if (Input.GetMouseButtonDown(0)) {
            Vector3 targetPosition = MouseWorld.GetPosition();
            GridPosition targetGridPosition = LevelGrid.Instance.GetGridPosition(targetPosition);

            if (selectedAction.IsValidActionGridPosition(targetGridPosition)) {
                SetBusy();
                selectedAction.TakeAction(targetGridPosition, ClearBusy);
            }
        }
    }

    private void SetBusy() {
        isBusy = true;
    }

    private void ClearBusy() {
        isBusy = false;
    }

    private bool TryHandleUnitSelection() {
        if (Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, unitLayerMask)) {
                if (hit.transform.TryGetComponent<Unit>(out Unit unit)) {
                    if (unit == selectedUnit) return false;

                    SetSelectedUnit(unit);
                    return true;
                }
            }
        }

        return false;
    }

    private void SetSelectedUnit(Unit unit) {
        selectedUnit = unit;
        SetSelectedAction(unit.GetMoveAction());
        OnSelectedUnitChange?.Invoke(this, EventArgs.Empty);
    }

    public void SetSelectedAction(BaseAction baseAction) {
        selectedAction = baseAction;
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

    public BaseAction GetSelectedAction() {
        return selectedAction;
    }
}
