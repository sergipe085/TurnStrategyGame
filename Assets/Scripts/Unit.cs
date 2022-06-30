using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    private Vector3 targetPosition = Vector3.zero;

    private void Update() {

        float stoppingDistance = 0.1f;
        if (Vector3.Distance(transform.position, targetPosition) > stoppingDistance) {
            Vector3 moveDirection = targetPosition - transform.position;
            moveDirection.Normalize();

            float moveSpeed = 4.0f;
            transform.position += moveDirection * Time.deltaTime * moveSpeed;
        }

        if (Input.GetMouseButtonDown(1)) {
            Move(MouseWorld.GetPosition());
        }
    }

    private void Move(Vector3 targetPosition) {
        this.targetPosition = targetPosition;
    }
}
