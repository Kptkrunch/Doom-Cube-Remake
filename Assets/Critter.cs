using System;
using System.Collections.Generic;
using Controllers;
using TechSkills;
using UnityEngine;

public class Critter : Tech
{
    public Transform player;
    public int critterLevel;
    public List<CritterStats> stats;
    private float _attackTimer;
    private float _moveTimer;
    private float _staminaTimer;

    private void Start()
    {
        _moveTimer = stats[critterLevel].moveInterval;
        _attackTimer = stats[critterLevel].attackInterval;
        _staminaTimer = stats[critterLevel].stamina;
    }

    private void Update()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, stats[critterLevel].searchRadius);
        if (hitColliders.Length > 0)
        {
            Collider2D closestCollider = hitColliders[0];
            float closestDistance = Vector3.Distance(transform.position, closestCollider.transform.position);
            foreach (Collider2D hitCollider in hitColliders)
            {
                if (hitCollider.CompareTag("Enemy"))
                {
                    float distanceToEnemy = Vector3.Distance(transform.position, hitCollider.transform.position);
                    if (distanceToEnemy <= stats[critterLevel].attackDistance)
                    {
                        _attackTimer -= Time.deltaTime;
                        if (_attackTimer <= 0)
                        {
                            Debug.Log("Attacking enemy: " + hitCollider.name);
                            if (hitCollider.gameObject)
                            {
                                hitCollider.GetComponent<EnemyController>().TakeDamage(stats[critterLevel].damage);
                            }
                            _attackTimer = stats[critterLevel].attackInterval;
                        }
                    }
                    else if (distanceToEnemy < closestDistance)
                    {
                        closestCollider = hitCollider;
                        closestDistance = distanceToEnemy;
                    }
                }
            }
            transform.position = Vector3.MoveTowards(transform.position, closestCollider.transform.position, stats[critterLevel].speed * Time.deltaTime);
            
        }
        else
        {
            Vector3 targetPosition = player.position - player.forward * stats[critterLevel].followDistance;
            transform.position = Vector3.Lerp(transform.position, targetPosition, stats[critterLevel].speed * Time.deltaTime);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        var position = transform.position;
        Gizmos.DrawWireSphere(position, stats[critterLevel].searchRadius);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(position, stats[critterLevel].attackDistance);
    }

    private void FollowPlayer()
    {
        Vector3 targetPosition = player.position - player.forward * stats[critterLevel].followDistance;
        transform.position = Vector3.Lerp(transform.position, targetPosition, stats[critterLevel].speed * Time.deltaTime);
    }

    private void MoveOnInterval(Transform target)
    {
        _moveTimer -= Time.deltaTime;
        if (_moveTimer <= 0)
        {
            _staminaTimer = stats[critterLevel].stamina;
            _staminaTimer -= Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target.position, stats[critterLevel].speed * Time.deltaTime);
            if (_staminaTimer <= 0)
            {
                _moveTimer = stats[critterLevel].moveInterval;
            }
        }
    }
}



[System.Serializable]
public class CritterStats
{
    public float health, damage, speed, followDistance, searchRadius, attackDistance, attackInterval, moveInterval, stamina, lifeTime;
}

