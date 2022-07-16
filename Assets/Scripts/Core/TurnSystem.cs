using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnSystem : MonoBehaviour
{
    public static TurnSystem Instance;

    public event EventHandler OnTurnChanged = null;

    private int currentTurn = 1;
    private bool isPlayerTurn = true;

    private void Awake() {
        Instance = this;
    }

    public void NextTurn() {
        currentTurn++;
        isPlayerTurn = !isPlayerTurn;
        OnTurnChanged?.Invoke(this, null);
    }

    public int GetTurn() {
        return currentTurn;
    }

    public bool IsPlayerTurn() {
        return isPlayerTurn;
    }
}
