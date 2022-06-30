using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridDebugObject : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshPro textMeshPro = null;
    private GridObject gridObject;

    public void SetGridObject(GridObject _gridObject) {
        gridObject = _gridObject;

        
    }

    private void Update() {
        textMeshPro.text = gridObject.ToString();
    }
}
