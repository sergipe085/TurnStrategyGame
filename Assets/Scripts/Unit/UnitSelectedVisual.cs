using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSelectedVisual : MonoBehaviour
{
    [SerializeField] private Unit unit = null;

    private MeshRenderer meshRenderer = null;

    private void Awake() {
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.enabled = false;
    }

    private void Start() {
        UnitActionSystem.Instance.OnSelectedUnitChange += UnitActionSystem_OnSelectedUnitChange;
        UpdateVisual();
    }

    private void UnitActionSystem_OnSelectedUnitChange(object sender, EventArgs empty) {
        UpdateVisual();
    }

    private void UpdateVisual() {
        Unit selectedUnit = UnitActionSystem.Instance.GetSelectedUnit();
        if (selectedUnit == unit) {
            meshRenderer.enabled = true;
        }
        else {
            meshRenderer.enabled = false;
        }
    }
}
