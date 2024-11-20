using System;
using System.Collections;
using Controllers;
using Controllers.Pools;
using FunkyCode;
using ScriptableObjs.TechWeaponsSOS;
using UnityEngine;
using Weapons.Projectiles;

namespace TechSkills
{
    public class BasicCritterTech : Tech
    {
        public float speed, health;
        public float attackRange, aggroRange, attackSpeed;
        public int pid;
        public LayerMask enemyLayer;
        public Sprite critterSprite;
        public TechWeaponData basicCritterData;

        [SerializeField] private Transform player;
        private Vector2 _targetPosition;
        private Collider2D[] _enemiesInRange = new Collider2D[10];
        private float _maxHealth, _distanceToTarget;
        private bool _foundTarget, _canAttack;
        private int _numOfEnemiesInRange;

        private void Awake()
        {
            _numOfEnemiesInRange = 0;
            ResetCritterAttackParams();
            SetStats();
        }

        private void FixedUpdate()
        {
            // Update critter to find a target to attack
            if (!_foundTarget)
            {
                _numOfEnemiesInRange =
                    Physics2D.OverlapCircleNonAlloc(transform.position, aggroRange, _enemiesInRange, enemyLayer);
                if (_numOfEnemiesInRange > 0 && _enemiesInRange[0])
                {
                    _targetPosition = _enemiesInRange[0].transform.position;
                    _foundTarget = true;
                    Debug.Log("target position: " + _targetPosition);
                }
            }
            
            // Judge distance to a found target and attempt to move close enough to attack
            if (_foundTarget && _targetPosition != Vector2.zero)
            {
                _distanceToTarget = Vector2.Distance(transform.position, _targetPosition);

                if (_distanceToTarget > attackRange)
                {
                    MaybeMoveTowardsTarget();
                } else if (_canAttack && _distanceToTarget <= attackRange)
                {
                    StartCoroutine(AttackLoop());
                }
            }


            // Check all for null then start attack loop coroutine
            if (_canAttack && _distanceToTarget < attackRange)
            {
                StartCoroutine(AttackLoop());
            }  

        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (_numOfEnemiesInRange < 1 && other.CompareTag("Enemy"))
            {
                _foundTarget = false;
                _targetPosition = Vector2.zero;
            }
        }

        private IEnumerator AttackLoop()
        {
            _canAttack = false;
            if (_foundTarget)
            {
                var attack = ProjectilePoolManager.poolProj.projPools[pid].GetPooledGameObject();
                attack.transform.position = _targetPosition;
                attack.SetActive(true);
                MusicManager.Instance.sfxCritter.FeedbacksList[id].Play(transform.position);
            }

            yield return new WaitForSeconds(attackSpeed);
            _canAttack = true;
            ResetCritterAttackParams();
        }

        private void MaybeMoveTowardsTarget()
        {
            var position = transform.position;
            position = Vector2.MoveTowards(position, _targetPosition, speed * Time.deltaTime);
            var transform1 = transform;
            transform1.position = position;
        }

        private void SetStats()
        {

            attackRange = basicCritterData.range;
            attackSpeed = basicCritterData.fireRate;
            pid = basicCritterData.pid;
            
            if (_maxHealth == 0) _maxHealth = health;
            health = _maxHealth;
        }

        private void ResetCritterAttackParams()
        {
            _targetPosition = Vector2.zero;

            _canAttack = true;
            _foundTarget = false;
        }
    }
}