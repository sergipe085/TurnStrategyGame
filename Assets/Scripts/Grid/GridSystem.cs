using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem
{
    private readonly int width = 0;
    private readonly int height = 0;
    private readonly float cellSize = 0.0f;
    private GridObject[,] gridObjectArray;

    public GridSystem(int _width, int _height, float _cellSize) {
        this.width = _width;
        this.height = _height;
        this.cellSize = _cellSize;

        gridObjectArray = new GridObject[width, height];

        for (int w = 0; w < width; w++) {
            for (int h = 0; h < height; h++) {
                GridPosition gridPosition = new GridPosition(w, h);
                gridObjectArray[w, h] = new GridObject(this, gridPosition);
            }
        }
    }

    public Vector3 GetWorldPosition(GridPosition gridPosition) {
        return new Vector3(gridPosition.x, 0, gridPosition.z) * cellSize;
    }

    public GridPosition GetGridPosition(Vector3 worldPosition) {
        return new GridPosition(Mathf.RoundToInt(worldPosition.x / cellSize), 
                                Mathf.RoundToInt(worldPosition.z / cellSize));
    }

    public void CreateDebugObjects(Transform debugPrefab) {
        for (int w = 0; w < width; w++) {
            for (int h = 0; h < height; h++) {
                GridPosition gridPosition = new GridPosition(w, h);

                Transform debugObject = GameObject.Instantiate(debugPrefab, GetWorldPosition(gridPosition), Quaternion.identity);
                GridDebugObject gridDebugObject = debugObject.GetComponent<GridDebugObject>();
                gridDebugObject.SetGridObject(GetGridObject(gridPosition));
            }
        }
    }

    public GridObject GetGridObject(GridPosition gridPosition) {
        return gridObjectArray[gridPosition.x, gridPosition.z];
    }

    public bool IsGridPositionValid(GridPosition gridPosition) {
        if (gridPosition.x >= width || gridPosition.x < 0) return false;
        if (gridPosition.z >= height || gridPosition.z < 0) return false;
        return true;
    }
}
