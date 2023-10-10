using System;
using System.Collections.Generic;
using Controllers;
using UnityEngine;

namespace TechSkills
{
    public class Critter : Tech
    {
        public Transform player;
        public int critterLevel;
        public List<CritterStats> stats;
        private float _attackTimer;

        private void Start()
        {
            _attackTimer = stats[critterLevel].attackInterval;
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
    }



    [System.Serializable]
    public class CritterStats
    {
        public float health, damage, speed, followDistance, maxFollowDistance, searchRadius, attackDistance, attackInterval, lifeTime;
    }
}