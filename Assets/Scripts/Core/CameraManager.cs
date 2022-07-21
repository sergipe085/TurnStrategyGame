using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private GameObject actionCameraObject = null;

    private void Start() {
        BaseAction.OnAnyActionStarted += HandleAnyActionStarted;
        BaseAction.OnAnyActionCompleted += HandleAnyActionCompleted;

        HideActionCamera();
    }

    private void ShowActionCamera() {
        actionCameraObject.SetActive(true);
    }

    private void HideActionCamera() {
        actionCameraObject.SetActive(false);
    }

    private void HandleAnyActionStarted(object sender, EventArgs n) {
        switch(sender) {
            case ShootAction shootAction:
                ShowActionCamera();

                Unit shooterUnit = shootAction.GetUnit();
                Unit targetUnit = shootAction.GetTargetUnit();

                Vector3 cameraCharacterHeight = Vector3.up * 1.7f;

                Vector3 shootDir = (targetUnit.transform.position - shooterUnit.transform.position).normalized;

                float shoulderOffsetAmount = 0.5f;
                Vector3 shoulderOffset = Quaternion.Euler(0f, 90f, 0f) * shootDir * shoulderOffsetAmount;

                Vector3 actionCameraPosition = shooterUnit.transform.position + cameraCharacterHeight + shoulderOffset + (shootDir * -1);

                actionCameraObject.transform.position = actionCameraPosition;
                actionCameraObject.transform.LookAt(targetUnit.transform.position + cameraCharacterHeight);
                break;
        }
    }

    private void HandleAnyActionCompleted(object sender, EventArgs n) {
        switch(sender) {
            case ShootAction:
                HideActionCamera();
                break;
        }
    }
}
