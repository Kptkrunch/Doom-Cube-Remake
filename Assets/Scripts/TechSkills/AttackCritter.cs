using Controllers;
using UnityEngine;

namespace TechSkills
{
    public class AttackCritter : Tech
    {
        public float speed, attackRange, health, damage, attackSpeed;
        public LayerMask enemyLayer;
        public Sprite critterSprite;
        public string damageType;

        [SerializeField] private Transform player;
        private Vector2 _targetPosition;
        private Collider2D[] _enemiesInRange = new Collider2D[10];
        private float _attackTimer;
        private bool _foundTarget;

        private void Start()
        {
            _targetPosition = transform.position;
        }

        private void Update()
        {
            _attackTimer -= Time.deltaTime;
            // Check for enemies in range
            int numEnemiesInRange = Physics2D.OverlapCircleNonAlloc(transform.position, attackRange, _enemiesInRange, enemyLayer);

            // Attack enemies in range
            for (int i = 0; i < numEnemiesInRange; i++)
            {
                Collider2D enemy = _enemiesInRange[i];
                Vector2 direction = enemy.transform.position - transform.position;
                if (_enemiesInRange[i]) _foundTarget = true;

                // Move towards the enemy
                if (_foundTarget) transform.position = Vector2.MoveTowards(transform.position, _enemiesInRange[i].transform.position, speed * Time.deltaTime);

                // Attack the enemy
                if (_attackTimer <= 0)
                {
                    if (Vector2.Distance(transform.position, enemy.transform.position) < attackRange)
                    {
                        _attackTimer = attackSpeed;
                        if (enemy) enemy.GetComponent<EnemyController>().TakeDamage(damage, damageType);
                        if (!enemy) _foundTarget = false;
                    } 
                }

            }
            
            // Move towards the target position
            var position = transform.position;
            position = Vector2.MoveTowards(position, _targetPosition, speed * Time.deltaTime);
            var transform1 = transform;
            transform1.position = position;
        }
    }
}