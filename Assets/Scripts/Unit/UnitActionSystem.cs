using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnitActionSystem : MonoBehaviour
{
    public static UnitActionSystem Instance { get; private set; }

    [SerializeField] private int ACTION_POINTS_MAX = 3;

    public event EventHandler OnSelectedUnitChange;
    public event EventHandler OnSelectedActionChange;
    public event EventHandler<bool> OnBusyChanged;
    public event EventHandler OnActionStarted;

    [SerializeField] private Unit selectedUnit = null;
    [SerializeField] private LayerMask unitLayerMask;

    private BaseAction selectedAction = null;
    private bool isBusy = false;
    private int actionPoints = 2;

    private void Awake() {
        if (Instance != null) {
            Debug.LogError("There is more than one UnitActionSystem!" + transform + " - " + Instance);
            Destroy(this.gameObject);
            return;
        }
        
        Instance = this;
    }

    private void Start() {
        TurnSystem.Instance.OnTurnChanged += TurnSystem_OnTurnChanged;

        SetSelectedUnit(selectedUnit);
        ResetActionPoints();
    }

    private void Update() {
        
        if (isBusy) return;

        if (!TurnSystem.Instance.IsPlayerTurn()) return;

        if (EventSystem.current.IsPointerOverGameObject()) return;

        if (TryHandleUnitSelection()) return;

        HandleSelectedAction();
    }

    private void HandleSelectedAction() {
        if (Input.GetMouseButtonDown(0)) {
            if (!CanSpendActionPointsToTakeAction(selectedAction)) return;

            Vector3 targetPosition = MouseWorld.GetPosition();
            GridPosition targetGridPosition = LevelGrid.Instance.GetGridPosition(targetPosition);

            if (selectedAction.IsValidActionGridPosition(targetGridPosition)) {
                SetBusy();
                selectedAction.TakeAction(targetGridPosition, ClearBusy);
                SpendActionPoints(selectedAction.GetActionPointsCost());
                OnActionStarted?.Invoke(this, null);
            }
        }
    }

    private void SetBusy() {
        isBusy = true;
        OnBusyChanged?.Invoke(this, isBusy);
    }

    private void ClearBusy() {
        isBusy = false;
        OnBusyChanged?.Invoke(this, isBusy);
    }

    private bool TryHandleUnitSelection() {
        if (Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, unitLayerMask)) {
                if (hit.transform.TryGetComponent<Unit>(out Unit unit)) {
                    if (unit == selectedUnit || unit.IsEnemy()) return false;

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
        OnSelectedActionChange?.Invoke(this, EventArgs.Empty);
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

    private bool CanSpendActionPointsToTakeAction(BaseAction baseAction) {
        return actionPoints - baseAction.GetActionPointsCost() >= 0;
    }

    private void SpendActionPoints(int amount) {
        actionPoints = Mathf.Clamp(actionPoints - amount, 0, 10);
    }

    private bool TrySpendActionPoints(BaseAction baseAction) {
        if (!CanSpendActionPointsToTakeAction(baseAction)) return false;

        SpendActionPoints(baseAction.GetActionPointsCost());
        return true;
    }

    public int GetActionPoints() {
        return actionPoints;
    }

    private void ResetActionPoints() {
        actionPoints = ACTION_POINTS_MAX;
    }

    private void TurnSystem_OnTurnChanged(object sender, EventArgs empty) {
        ResetActionPoints();
    }
}
