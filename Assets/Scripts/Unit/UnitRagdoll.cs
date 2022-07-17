using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitRagdoll : MonoBehaviour
{
    [SerializeField] private Transform ragdollRootBone = null;

    public void Setup(Transform originalRootBone) {
        MatchAllChildTransforms(originalRootBone, ragdollRootBone);
    }

    private void MatchAllChildTransforms(Transform root, Transform clone) {
        foreach(Transform child in root) {
            Transform cloneToMatch = clone.Find(child.name);
            if (cloneToMatch) {
                cloneToMatch.position = child.position;
                cloneToMatch.rotation = child.rotation;

                MatchAllChildTransforms(child, cloneToMatch);
            }
        }
    }
}
