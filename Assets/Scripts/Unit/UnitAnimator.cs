using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAnimator : MonoBehaviour
{
    [SerializeField] private Animator animator = null;

    private readonly int IsWalkingHash = Animator.StringToHash("IsWalking");

    private void Awake() {
        if (TryGetComponent<MoveAction>(out MoveAction moveAction)) {
            moveAction.OnStartMoving += MoveAction_StartMoving;
            moveAction.OnStopMoving += MoveAction_StopMoving;
        }
    }

    private void MoveAction_StartMoving() {
        animator.SetBool(IsWalkingHash, true);
    }

    private void MoveAction_StopMoving() {
        animator.SetBool(IsWalkingHash, false);
    }
}
