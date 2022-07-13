using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private void OnEnable() {
        //UnitActionSystem.Instance.OnSelectedUnitChange += OnUnitSelectChange;
    }

    private void OnDisable() {
        //UnitActionSystem.Instance.OnSelectedUnitChange -= OnUnitSelectChange;
    }

    private void Update() {
        HandleMovement();
        HandleRotation();
    }

    private void OnUnitSelectChange(object sender, System.EventArgs args) {
        Unit selectedUnit = UnitActionSystem.Instance.GetSelectedUnit();
        transform.position = selectedUnit.transform.position;
    }

    private void HandleMovement() {
        Vector3 inputMoveDir = Vector3.zero;
        inputMoveDir.z = Input.GetAxisRaw("Vertical");
        inputMoveDir.x = Input.GetAxisRaw("Horizontal");

        Vector3 moveDirection = transform.forward * inputMoveDir.z + transform.right * inputMoveDir.x;
        moveDirection.Normalize();

        float moveSpeed = 4.0f;
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }

    private void HandleRotation() {
        if (Input.GetMouseButton(1))  {
            Vector2 rotationInput;
            rotationInput.x = Input.GetAxis("Mouse X");
            transform.eulerAngles += new Vector3(0f, rotationInput.x, 0f);
        }
    }
}
