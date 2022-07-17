using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAction : BaseAction
{
    private enum State {
        Aiming,
        Shooting, 
        Cooloff
    }

    [SerializeField] private int maxShootDistance = 1;

    private State state;
    private float stateTimer = 0.0f;
    private Unit targetUnit = null;
    private bool canShoot = true;

    private void Update() {
        if (!isActive) return;

        stateTimer -= Time.deltaTime;

        switch(state) {
            case State.Aiming:
                Vector3 aimDir = (targetUnit.transform.position - transform.position).normalized;
                float rotateSpeed = 10.0f;
                transform.forward = Vector3.Lerp(transform.forward, aimDir, Time.deltaTime * rotateSpeed);
                break;
            case State.Shooting:
                if (canShoot) {
                    Shoot();
                    canShoot = false;
                }
                break;
            case State.Cooloff:
                break;
        }

        if (stateTimer <= 0.0f) {
            NextState();
        }
    }

    private void Shoot() {
        targetUnit.Damage();
    }

    private void NextState() {
        switch(state) {
            case State.Aiming:
                state = State.Shooting;
                float shootingStateTime = 0.5f;
                stateTimer = shootingStateTime;
                break;
            case State.Shooting:
                state = State.Cooloff;
                float cooloffStateTime = 0.5f;
                stateTimer = cooloffStateTime;
                break;
            case State.Cooloff:
                ActionComplete();
                break;
        }

        Debug.Log(state);
    }

    public override void TakeAction(GridPosition gridPosition, Action onCompleteFunction) {
        base.TakeAction(gridPosition, onCompleteFunction);
        canShoot = true;

        targetUnit = LevelGrid.Instance.GetUnitAtGridPosition(gridPosition);
        
        state = State.Aiming;
        float aimingStateTime = 0.5f;
        stateTimer = aimingStateTime;
        Debug.Log("Aiming");

    }

    public override string GetActionName() {
        return "SHOOT";
    }

    public override List<GridPosition> GetValidActionGridPositionList() {
        List<GridPosition> validGridPositionList = new List<GridPosition>();

        GridPosition unitGridPosition = unit.GetGridPosition();

        for (int x = -maxShootDistance; x <= maxShootDistance; x++) {
            for (int z = -maxShootDistance; z <= maxShootDistance; z++) {
                GridPosition offsetGridPosition = new GridPosition(x, z);
                GridPosition testGridPosition = unitGridPosition + offsetGridPosition;

                if (!LevelGrid.Instance.IsGridPositionValid(testGridPosition)) {
                    continue;
                }

                int testDistance = Mathf.Abs(x) + Mathf.Abs(z);
                if (testDistance > maxShootDistance) {
                    continue;
                }

                if (testGridPosition == unitGridPosition) {
                    continue;
                }

                Unit testUnit = LevelGrid.Instance.GetUnitAtGridPosition(testGridPosition);

                if (testUnit && testUnit.IsEnemy() != unit.IsEnemy()) {
                    validGridPositionList.Add(testGridPosition);
                }
            }
        }

        return validGridPositionList;
    }
}
