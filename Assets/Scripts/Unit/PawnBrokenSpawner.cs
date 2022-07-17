using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnBrokenSpawner : MonoBehaviour
{
    [SerializeField] private Transform pawnBrokenPrefab = null;
    private HealthSystem healthSystem = null;

    private void Awake() {
        healthSystem = GetComponent<HealthSystem>();

        healthSystem.OnDieEvent += HealthSystem_OnDie;
    }

    private void HealthSystem_OnDie() {
        Instantiate(pawnBrokenPrefab, transform.position, Quaternion.identity);
    }
}
