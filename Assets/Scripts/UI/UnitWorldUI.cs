using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitWorldUI : MonoBehaviour
{
    [SerializeField] private HealthSystem health = null;
    [SerializeField] private Slider healthBarSlider = null;

    private void Start() {
        health.OnTakeDamage += HealthSystem_OnTakeDamage;
        UpdateSlider();
    }

    private void HealthSystem_OnTakeDamage() {
        UpdateSlider();
    }

    private void UpdateSlider() {
        healthBarSlider.value = health.GetHealthPercentage();
    }
}
