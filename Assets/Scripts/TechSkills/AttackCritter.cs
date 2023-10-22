using Controllers;
using UnityEngine;

namespace TechSkills
{
    public class AttackCritter : Tech
    {
        public float speed, minFollowDist, maxFollowDist, attackRange, aggreRadius, health, damage, attackSpeed;
        public LayerMask enemyLayer;
        public Sprite critterSprite;

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
                Debug.DrawRay(transform.position, direction, Color.red);
                if (_enemiesInRange[i]) _foundTarget = true;
                if (_foundTarget) Debug.DrawRay(transform.position, direction, Color.red);

                // Move towards the enemy
                if (_foundTarget == true) transform.position = Vector2.MoveTowards(transform.position, _enemiesInRange[i].transform.position, speed * Time.deltaTime);

                // Attack the enemy
                if (_attackTimer <= 0)
                {
                    if (Vector2.Distance(transform.position, enemy.transform.position) < attackRange)
                    {
                        _attackTimer = attackSpeed;
                        if (enemy) enemy.GetComponent<EnemyController>().TakeDamage(damage);
                        if (!enemy) _foundTarget = false;
                    } 
                }

            }
            
            // Follow the player
            if (Vector2.Distance(transform.position, player.position) > minFollowDist && Vector2.Distance(transform.position, player.position) < maxFollowDist)
            {
                _targetPosition = player.position;
            }

            // Move towards the target position
            var position = transform.position;
            position = Vector2.MoveTowards(position, _targetPosition, speed * Time.deltaTime);
            var transform1 = transform;
            transform1.position = position;
            
            

        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            var position = transform.position;
            Gizmos.DrawWireSphere(position, minFollowDist);
            
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(position, maxFollowDist);

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(position, attackRange); 
            
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(position, aggreRadius);
        }
    }
}