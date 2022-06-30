using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    [SerializeField] private Transform debugObjectPrefab = null;
    private GridSystem gridSystem = null;

    private void Start() {
        gridSystem = new GridSystem(16, 16, 2f);
        gridSystem.CreateDebugObjects(debugObjectPrefab);
    }
}
