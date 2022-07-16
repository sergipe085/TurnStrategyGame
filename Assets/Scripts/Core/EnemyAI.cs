using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private float timer = 0.0f;

    private void Update() {
        if (TurnSystem.Instance.IsPlayerTurn()) return;

        timer += Time.deltaTime;
        if (timer > 2.0f) {
            TurnSystem.Instance.NextTurn();
            timer = 0;
        }
    }
}
