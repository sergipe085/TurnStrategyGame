using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField] private bool isEnemy = false;

    private GridPosition gridPosition;
    private MoveAction moveAction = null;
    private SpinAction spinAction = null;
    private BaseAction[] baseActionArray;

    private void Awake() {
        moveAction = GetComponent<MoveAction>();
        spinAction = GetComponent<SpinAction>();
        baseActionArray = GetComponents<BaseAction>();
    }

    private void Start() {
        gridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        LevelGrid.Instance.AddUnitAtGridPosition(gridPosition, this);
    }

    private void Update() {
        GridPosition newGridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        if (newGridPosition != gridPosition) {
            LevelGrid.Instance.UnitMovedGridPosition(this, gridPosition, newGridPosition);
            gridPosition = newGridPosition;
        }
    }

    public MoveAction GetMoveAction() {
        return moveAction;
    }

    public SpinAction GetSpinAction() {
        return spinAction;
    }

    public GridPosition GetGridPosition() {
        return gridPosition;
    }

    public BaseAction[] GetBaseActionArray() {
        return baseActionArray;
    }

    public bool IsEnemy() {
        return isEnemy;
    }

    public void Damage() {
        Debug.Log(transform + " damaged");
        LevelGrid.Instance.RemoveUnitAtGridPosition(gridPosition, this);
        this.gameObject.SetActive(false);
    }
}
