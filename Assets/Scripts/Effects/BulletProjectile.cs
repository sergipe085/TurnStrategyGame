using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : MonoBehaviour
{
    [SerializeField] private TrailRenderer trailRenderer;
    [SerializeField] private Transform bulletHitVfxPrefab;
    private Vector3 targetPosition = Vector3.zero;

    public void Setup(Vector3 targetPosition) {
        this.targetPosition = targetPosition;
    }

    private void Update() {
        float projectileSpeed = 50.0f;

        float distanceBeforeMoving = Vector3.Distance(transform.position, targetPosition);

        Vector3 direction = targetPosition - transform.position;
        transform.position += projectileSpeed * Time.deltaTime * direction.normalized;

        float distanceAfterMoving = Vector3.Distance(transform.position, targetPosition);

        if (distanceAfterMoving > distanceBeforeMoving) {
            transform.position = targetPosition;
            trailRenderer.transform.SetParent(null);
            Instantiate(bulletHitVfxPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
