using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitActionSystem : MonoBehaviour
{
    [SerializeField] private Unit selectedUnit = null;
    [SerializeField] private LayerMask unitLayerMask;

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            if (!TryHandleUnitSelection())
                MoveSelectedUnit();
        }
    }

    private bool TryHandleUnitSelection() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, unitLayerMask)) {
            if (hit.transform.TryGetComponent<Unit>(out Unit unit)) {
                selectedUnit = unit;
                return true;
            }
        }

        return false;
    }

    private void MoveSelectedUnit() {
        if (!selectedUnit) return;

        Vector3 targetPosition = MouseWorld.GetPosition();
        selectedUnit.Move(targetPosition);
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
}
