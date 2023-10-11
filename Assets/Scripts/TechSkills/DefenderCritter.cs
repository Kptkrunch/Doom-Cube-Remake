using System;
using System.Collections.Generic;
using Controllers;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace TechSkills
{
    public class DefenderCritter : Tech
    {
        public Transform player;
        private Transform _critter;
        public int critterLevel;
        public List<DefenderStats> stats;
        private GameObject[] _enemies;
        private GameObject _currentTarget;
        private readonly float _lastDamageTime = -1f;
        private float _lifeTime, _cooldownTimer;

        void Start()
        {
            InitializeVariables();
            _cooldownTimer = 0;
        }

        private void FixedUpdate()
        {
            DieInTime();
            float distanceToPlayer = Vector3.Distance(_critter.position, player.position);

            if (distanceToPlayer > stats[critterLevel].followDistance + 2)
            {
                LookForAndAttackEnemies();
            }
            else if (distanceToPlayer > stats[critterLevel].followDistance && distanceToPlayer < stats[critterLevel].maxFollowDistance)
            {
                // Follow the player
                _critter.Translate((player.position - _critter.position).normalized * (stats[critterLevel].speed * Time.deltaTime));
            }
            else
            {
                // Defend the player
                Debug.Log("Defending player");
            }
            AggroOnEnemyWhoDmgPlayer();
        }

        private void OnEnable()
        {
            _lifeTime = stats[critterLevel].lifeTime;
        }

        private void InitializeVariables()
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            _critter = transform;
            _enemies = GameObject.FindGameObjectsWithTag("Enemy");
            _lifeTime = stats[critterLevel].lifeTime;
        }
    
        private void DieInTime()
        {
            if (_lifeTime <= 0) gameObject.SetActive(false);
            _lifeTime -= Time.deltaTime;
        }

        private void AggroOnEnemyWhoDmgPlayer()
        {
            // Check for recent damage to the player
            if (Time.time - _lastDamageTime < 5f)
            {
                // Look for the enemy that caused damage
                foreach (GameObject enemy in _enemies)
                {
                    if (enemy.GetComponent<PlayerHealthController>().lastAttackedTime > _lastDamageTime)
                    {
                        _currentTarget = enemy.gameObject;
                        break;
                    }
                }
            }
        }

        private void LookForAndAttackEnemies()
        {
            // Look for enemies
            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(_critter.position, stats[critterLevel].searchRadius);
            int i = 0;
            while (i < hitColliders.Length)
            {
                if (hitColliders[i].CompareTag("Enemy"))
                {
                    // Attack the enemy
                    if (!_currentTarget || _currentTarget != hitColliders[i].gameObject)
                    {
                        _currentTarget = hitColliders[i].gameObject;
                    }
                    if (Vector3.Distance(_critter.position, _currentTarget.transform.position) > stats[critterLevel].attackDistance)
                    {
                        _critter.Translate((_currentTarget.transform.position - _critter.position).normalized * (stats[critterLevel].speed * Time.deltaTime));
                    }
                    else
                    {
                        hitColliders[i].GetComponent<EnemyController>().TakeDamage(stats[critterLevel].damage);
                    }
                    return;
                }
                i++;
            }
        }
    }

    [System.Serializable]
    public class DefenderStats
    {
        public float health, damage, speed, followDistance, maxFollowDistance, searchRadius, attackDistance, attackInterval, moveInterval, stamina, lifeTime;
    }
}