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
        float angleToEnemy = Vector2.Angle(directionToEnemy, transform.up);

        if (angleToEnemy <= detectionAngle / 2f && directionToEnemy.magnitude <= detectionRange)
        {
            return true;
        }

        return false;
    }

    public GameObject FindClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector2.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < closestDistance)
            {
                closestDistance = distanceToEnemy;
                closestEnemy = enemy;
            }
        }

        return closestEnemy;
    }
}