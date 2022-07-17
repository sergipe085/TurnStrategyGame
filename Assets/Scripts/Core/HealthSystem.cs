using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private int health = 100;

    public event Action OnDieEvent = null;

    public void TakeDamage(int damageAmoung) {
        health -= damageAmoung;

        if (health <= 0) {
            health = 0;

            Die();
        }
    }

    private void Die() {
        OnDieEvent?.Invoke();
    }
}
