using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAnimator : MonoBehaviour
{
    [SerializeField] private Animator animator = null;
    [SerializeField] private BulletProjectile bulletProjectilePrefab = null;

    private readonly int IsWalkingHash = Animator.StringToHash("IsWalking");
    private readonly int ShootHash = Animator.StringToHash("Shoot");

    private void Awake() {
        if (TryGetComponent<MoveAction>(out MoveAction moveAction)) {
            moveAction.OnStartMoving += MoveAction_StartMoving;
            moveAction.OnStopMoving += MoveAction_StopMoving;
        }

        if (TryGetComponent<ShootAction>(out ShootAction shootAction)) {
            shootAction.OnShootEvent += ShootAction_OnShoot;
        }
    }

    private void MoveAction_StartMoving() {
        animator.SetBool(IsWalkingHash, true);
    }

    private void MoveAction_StopMoving() {
        animator.SetBool(IsWalkingHash, false);
    }

    private void ShootAction_OnShoot(Unit targetUnit) {
        animator.SetTrigger(ShootHash);

        Vector3 shootPosition = transform.position + Vector3.up;
        BulletProjectile bulletProjectile = Instantiate(bulletProjectilePrefab, shootPosition, Quaternion.identity);

        Vector3 targetPosition = targetUnit.transform.position;
        targetPosition.y = shootPosition.y;

        bulletProjectile.Setup(targetPosition);
    }
}
