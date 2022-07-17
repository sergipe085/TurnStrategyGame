using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitRagdollSpawner : MonoBehaviour
{
    [SerializeField] private UnitRagdoll ragdollPrefab = null;
    [SerializeField] private Transform originalRootBone = null;
    private HealthSystem healthSystem = null;

    private void Awake() {
        healthSystem = GetComponent<HealthSystem>();

        healthSystem.OnDieEvent += HealthSystem_OnDie;
    }

    private void HealthSystem_OnDie() {
        UnitRagdoll ragdoll = Instantiate(ragdollPrefab, transform.position, transform.rotation);
        ragdoll.Setup(originalRootBone);
    }
}
