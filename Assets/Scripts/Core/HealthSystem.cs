using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int currentHealth = 100;

    public event Action OnDieEvent = null;
    public event Action OnTakeDamage = null;

    private void Start() {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damageAmoung) {
        currentHealth = Mathf.Clamp(currentHealth - damageAmoung, 0, maxHealth);

        if (currentHealth <= 0) {
            Die();
        }

        OnTakeDamage?.Invoke();
    }

    public float GetHealthPercentage() {
        return (float)currentHealth/maxHealth;
    }

    private void Die() {
        OnDieEvent?.Invoke();
    }
}
