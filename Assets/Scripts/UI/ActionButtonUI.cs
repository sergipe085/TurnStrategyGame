using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ActionButtonUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMeshPro = null;
    [SerializeField] private Button button = null;

    public void SetBaseAction(BaseAction baseAction) {
        textMeshPro.text = baseAction.GetActionName();

        button.onClick.AddListener(() => {
            UnitActionSystem.Instance.SetSelectedAction(baseAction);
        });
    }
}
