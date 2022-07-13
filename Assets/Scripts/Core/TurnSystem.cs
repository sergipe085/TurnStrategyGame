using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnSystem : MonoBehaviour
{
    public static TurnSystem Instance;

    public event EventHandler OnTurnChanged = null;

    private int currentTurn = 1;

    private void Awake() {
        Instance = this;
    }

    public void NextTurn() {
        currentTurn++;
        OnTurnChanged?.Invoke(this, null);
    }

    public int GetTurn() {
        return currentTurn;
    }
}
