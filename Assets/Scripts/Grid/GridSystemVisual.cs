using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystemVisual : MonoBehaviour
{
    public static GridSystemVisual Instance = null;

    [SerializeField] private Transform gridSystemVisualSinglePrefab = null;
    private GridSystemVisualSingle[,] gridSystemVisualSingles;

    [Serializable]
    public struct GridVisualTypeMaterial {
        public GridType gridType;
        public Material material;
    }

    [SerializeField] private List<GridVisualTypeMaterial> gridVisualTypeMaterials = new();

    public enum GridType {
        White, 
        Blue
    }

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        int width = LevelGrid.Instance.GetWidth();
        int height = LevelGrid.Instance.GetHeight();

        gridSystemVisualSingles = new GridSystemVisualSingle[width, height];

        for (int x = 0; x < width; x++) {
            for (int z = 0; z < height; z++) {
                GridPosition gridPosition = new GridPosition(x, z);
                Transform gridVisualObject = Instantiate(gridSystemVisualSinglePrefab, LevelGrid.Instance.GetWorldPosition(gridPosition), Quaternion.identity);
                gridSystemVisualSingles[x, z] = gridVisualObject.GetComponent<GridSystemVisualSingle>();
            }
        }

        UnitActionSystem.Instance.OnSelectedActionChange += HandleOnSelectedActionChange;
        LevelGrid.OnAnyUnitMovePosition += UpdateGridVisual;
        UpdateGridVisual();
    }

    private void UpdateGridVisualSingleColors(BaseAction baseAction) {
        foreach(GridSystemVisualSingle gridSystemVisualSingle in gridSystemVisualSingles) {
            Material mat = gridVisualTypeMaterials.Find(a => a.gridType == baseAction.gridType).material;
            gridSystemVisualSingle.UpdateMaterial(mat);
        }
    }

    private void HandleOnSelectedActionChange(object sender, EventArgs a) {
        UpdateGridVisual();
        UpdateGridVisualSingleColors(UnitActionSystem.Instance.GetSelectedAction());
    }

    public void HideAllGridPosition() {
        foreach(GridSystemVisualSingle gridSystemVisualSingle in gridSystemVisualSingles) {
            gridSystemVisualSingle.Hide();
        }
    }

    public void ShowGridPositionList(List<GridPosition> gridPositionList) {
        foreach(GridPosition gridPosition in gridPositionList) {
            gridSystemVisualSingles[gridPosition.x, gridPosition.z].Show();
        }
    }

    public void UpdateGridVisual() {
        HideAllGridPosition();

        BaseAction selectedAction = UnitActionSystem.Instance.GetSelectedAction();
        if (!selectedAction) return;
        List<GridPosition> validList = selectedAction.GetValidActionGridPositionList();

        ShowGridPositionList(validList);
    }
}
