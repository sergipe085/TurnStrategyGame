using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitRagdoll : MonoBehaviour
{
    [SerializeField] private Transform ragdollRootBone = null;

    public void Setup(Transform originalRootBone) {
        MatchAllChildTransforms(originalRootBone, ragdollRootBone);
        AddExplosionToRagdoll(ragdollRootBone, 300.0f, transform.position, 10.0f);
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

    private void AddExplosionToRagdoll(Transform root, float explosionForce, Vector3 explosionPosition, float explosionRange) {
        foreach(Transform child in root) {
            if (child.TryGetComponent<Rigidbody>(out Rigidbody childRig)) {
                childRig.AddExplosionForce(explosionForce, explosionPosition, explosionRange);
                AddExplosionToRagdoll(child, explosionForce, explosionPosition, explosionRange);
            }
        }
    }
}
