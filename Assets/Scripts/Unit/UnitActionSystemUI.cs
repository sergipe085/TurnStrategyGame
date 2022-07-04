using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitActionSystemUI : MonoBehaviour
{
    [SerializeField] private Transform actionButtonPrefab;
    [SerializeField] private Transform actionButtonContainer;

    private void Start() {
        UnitActionSystem.Instance.OnSelectedUnitChange += UnitActionSystem_OnSelectUnitChanged;
        CreateUnitActionButtons();
    }

    private void CreateUnitActionButtons() {
        foreach(Transform buttonTransform in actionButtonContainer) {
            Destroy(buttonTransform.gameObject);
        }

        Unit selectedUnit = UnitActionSystem.Instance.GetSelectedUnit();
        if (!selectedUnit) return;

        BaseAction[] actions = selectedUnit.GetBaseActionArray();

        foreach(BaseAction action in actions) {
            Transform actionButtonTransform = Instantiate(actionButtonPrefab, actionButtonContainer);
            ActionButtonUI actionButtonUI = actionButtonTransform.GetComponent<ActionButtonUI>();
            actionButtonUI.SetBaseAction(action);
        }
    }

    private void UnitActionSystem_OnSelectUnitChanged(object sender, EventArgs empty) {
        CreateUnitActionButtons();
    }
}
