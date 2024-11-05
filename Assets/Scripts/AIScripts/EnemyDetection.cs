using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    public float detectionRange = 5f;
    public float detectionAngle = 45f;

    public bool IsEnemyInCone(GameObject enemy)
    {
        Vector2 directionToEnemy = enemy.transform.position - transform.position;
        var angleToEnemy = Vector2.Angle(directionToEnemy, transform.up);

        if (angleToEnemy <= detectionAngle / 2f && directionToEnemy.magnitude <= detectionRange) return true;

        return false;
    }

    public GameObject FindClosestEnemy()
    {
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closestEnemy = null;
        var closestDistance = Mathf.Infinity;

        foreach (var enemy in enemies)
        {
            var distanceToEnemy = Vector2.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < closestDistance)
            {
                closestDistance = distanceToEnemy;
                closestEnemy = enemy;
            }
        }

        return closestEnemy;
    }
}