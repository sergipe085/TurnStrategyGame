using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseWorld : MonoBehaviour
{
    public static MouseWorld instance = null;

    [SerializeField] private LayerMask mousePlaneLayerMask;

    private void Awake() {
        instance = this;
    }

    public static Vector3 GetPosition() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, instance.mousePlaneLayerMask);
        return hit.point;
    }
}
