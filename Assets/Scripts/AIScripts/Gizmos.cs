using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretGizmos : MonoBehaviour
{
    public float detectionRange = 5f;
    public float detectionAngle = 45f;

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        Gizmos.color = Color.red;
        Vector3 direction1 = transform.rotation * Vector3.up * detectionRange * Mathf.Tan(detectionAngle / 2f * Mathf.Deg2Rad);
        Gizmos.DrawLine(transform.position, transform.position + direction1);

        Vector3 direction2 = transform.rotation * Vector3.up * detectionRange * Mathf.Tan(-detectionAngle / 2f * Mathf.Deg2Rad);
        Gizmos.DrawLine(transform.position, transform.position + direction2);
    }
}