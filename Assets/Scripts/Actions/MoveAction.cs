using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAction : MonoBehaviour
{
    [SerializeField] private Animator unitAnimator = null;
    [SerializeField] private int maxMoveDistance = 0;

    private readonly int IsWalkingHash = Animator.StringToHash("IsWalking");

    private Vector3 targetPosition = Vector3.zero;
    private Unit unit = null;

    private void Awake() {
        unit = GetComponent<Unit>();
        targetPosition = transform.position;
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
        }
        else {
            unitAnimator.SetBool(IsWalkingHash, false);
        }
    }

    public void Move(Vector3 _targetPosition) {
        this.targetPosition = _targetPosition;
    }

    public List<GridPosition> GetValidActionGridPositionList() {
        List<GridPosition> validGridPositions = new List<GridPosition>();
        GridPosition unitGridPosition = unit.GetGridPosition();

        for (int x = -maxMoveDistance; x <= maxMoveDistance; x++) {
            for (int z = -maxMoveDistance; z <= maxMoveDistance; z++) {
                GridPosition offsetGridPosition = new GridPosition(x, z);
                GridPosition testGridPosition = unitGridPosition + offsetGridPosition;

                if (LevelGrid.Instance.IsGridPositionValid(testGridPosition)) {
                    validGridPositions.Add(testGridPosition);
                    Debug.Log(testGridPosition);
                }
            }   
        }

        return validGridPositions;
    }
}
