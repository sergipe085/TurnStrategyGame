using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private Animator unitAnimator = null;
    private Vector3 targetPosition = Vector3.zero;
    private GridPosition gridPosition;

    private readonly int IsWalkingHash = Animator.StringToHash("IsWalking");

    private void Awake() {
        targetPosition = transform.position;
    }

    private void Start() {
        gridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        LevelGrid.Instance.AddUnitAtGridPosition(gridPosition, this);
    }

    private void Update() {

        float stoppingDistance = 0.1f;
        if (Vector3.Distance(transform.position, targetPosition) > stoppingDistance) {
            Vector3 moveDirection = targetPosition - transform.position;
            moveDirection.Normalize();

            float moveSpeed = 4.0f;
            transform.position += moveDirection * Time.deltaTime * moveSpeed;

            float rotateSpeed = 20.0f;
            transform.forward = Vector3.Lerp(transform.forward, moveDirection, Time.deltaTime * rotateSpeed);

            unitAnimator.SetBool(IsWalkingHash, true);

            GridPosition newGridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
            if (newGridPosition != gridPosition) {
                LevelGrid.Instance.UnitMovedGridPosition(this, gridPosition, newGridPosition);
                gridPosition = newGridPosition;
            }
        }
        else {
            unitAnimator.SetBool(IsWalkingHash, false);
        }
    }

    public void Move(Vector3 targetPosition) {
        this.targetPosition = targetPosition;
    }
}
