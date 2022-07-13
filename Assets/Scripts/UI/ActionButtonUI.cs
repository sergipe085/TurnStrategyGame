using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class ActionButtonUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMeshPro = null;
    [SerializeField] private Button button = null;
    [SerializeField] private GameObject selected = null;
    private BaseAction baseAction = null;

    public void SetBaseAction(BaseAction baseAction) {
        textMeshPro.text = baseAction.GetActionName();
        this.baseAction = baseAction;

        button.onClick.AddListener(() => {
            UnitActionSystem.Instance.SetSelectedAction(baseAction);
        });
    }

    public void UpdateSelectedVisual() {
        selected.SetActive(baseAction == UnitActionSystem.Instance.GetSelectedAction());
    }
}
