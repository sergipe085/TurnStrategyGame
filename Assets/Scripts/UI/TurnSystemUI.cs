using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnSystemUI : MonoBehaviour
{
    [SerializeField] private Button endTurnButton = null;
    [SerializeField] private TMPro.TextMeshProUGUI turnText = null;

    private void Start() {
        endTurnButton.onClick.AddListener(TurnSystem.Instance.NextTurn);
        TurnSystem.Instance.OnTurnChanged += TurnSystem_OnTurnChanged;

        UpdateTurnText();
    }

    public void TurnSystem_OnTurnChanged(object sender, EventArgs empty) {
        UpdateTurnText();
    }

    private void UpdateTurnText() {
        turnText.text = "TURN " + TurnSystem.Instance.GetTurn().ToString();
    }
}
