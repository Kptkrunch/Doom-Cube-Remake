using MoreMountains.Feedbacks;
using UnityEngine;

namespace Controllers
{
    public class EnemyController : MonoBehaviour
    {
        public Rigidbody2D rb2d;
        public float moveSpeed;
        public float damage;
        public float health = 5;
        
        private Transform _target;
        private float _hitCounter, _knockBackTimer, _hitInterval, _originalMoveSpeed;
        
        private void Start()
        {
            _originalMoveSpeed = moveSpeed;
            _target = PlayerHealthController.contPHealth.transform;
        }
    
        private void Update()
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
                PlayerHealthController.contPHealth.TakeDamage(damage);

                _hitCounter = _hitInterval;
            }
        }

        public void TakeDamage(float enemyDamage)
        {
            health -= enemyDamage;
            if (health <= 0)
            {
                ExperienceController.contExp.expDrop.DropItem(transform.position);
                Destroy(gameObject);
            }
            
            ShowDamage(enemyDamage);
        }

        public void KnockBack(float knockBackAmount, float knockBackDuration)
        {
            _knockBackTimer = knockBackDuration;
            if (_knockBackTimer >= 0)
            {
                moveSpeed = -moveSpeed * knockBackAmount;
            }
        }
        
                
        // ReSharper disable Unity.PerformanceAnalysis
        public void ShowDamage(float theDamage, float intensity = 1f)
        {
            MMF_FloatingText floatingText = DamageNumberController.contDmgText
                .player.GetFeedbackOfType<MMF_FloatingText>();
            floatingText.Value = theDamage.ToString();
            if (rb2d) DamageNumberController.contDmgText.player.PlayFeedbacks(transform.position);
        }
    }
}
