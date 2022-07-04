using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinAction : BaseAction
{
    private float totalSpinned = 0.0f;

    private void Update() {
        if (!isActive) return;

        float spinAddAmount = 360f * Time.deltaTime;
        transform.eulerAngles += new Vector3(0f, spinAddAmount, 0f);
        totalSpinned += spinAddAmount;

        if (totalSpinned >= 360f) {
            isActive = false;
            OnActionCompleteEvent?.Invoke();
        }
    }

    public void Spin(Action onCompleteFunction) {
        isActive = true;
        totalSpinned = 0.0f;
        OnActionCompleteEvent = onCompleteFunction;
    }

    public override string GetActionName() {
        return "SPIN";
    }
}
