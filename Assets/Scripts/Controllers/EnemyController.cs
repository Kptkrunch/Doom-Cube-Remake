using UnityEngine;

namespace Controllers
{
    public class EnemyController : MonoBehaviour
    {
        public Rigidbody2D rb2d;
        public float moveSpeed;
        private float _originalMoveSpeed;
        private Transform _target;

        public float damage;
        public float hitInterval;
        private float _hitCounter;

        private float _knockBackTimer;
    
        public float health = 5;
        void Start()
        {
            _originalMoveSpeed = moveSpeed;
            _target = PlayerHealthController.phcInstance.transform;
        }
    
        void Update()
        {
            rb2d.velocity = (_target.position - transform.position).normalized * moveSpeed;
            if (_hitCounter > 0f)
            {
                _hitCounter -= Time.deltaTime;
            }
        
            if (_knockBackTimer <= 0)
            {
                moveSpeed = _originalMoveSpeed;
            }
        
            if (_knockBackTimer > 0f)
            {
                _knockBackTimer -= Time.deltaTime;
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player") && _hitCounter <= 0f)
            {
                PlayerHealthController.phcInstance.TakeDamage(damage);

                _hitCounter = hitInterval;
            }
        }

        public void TakeDamage(float enemyDamage)
        {
            health -= enemyDamage;
            if (health <= 0)
            {
                ExperienceController.expController.expDrop.DropItem(transform.position);
                Destroy(gameObject);
            }
        
            DamageNumberController.dnController.ShowDamage(enemyDamage, transform.position);
        }

        public void KnockBack(float knockBackAmount, float knockBackDuration)
        {
            _knockBackTimer = knockBackDuration;
            if (_knockBackTimer >= 0)
            {
                moveSpeed = -moveSpeed * knockBackAmount;
            }
        }
    }
}
