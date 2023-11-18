using GenUtilsAndTools;
using MoreMountains.Feedbacks;
using TechSkills;
using UnityEngine;
using UnityEngine.Serialization;

namespace Controllers
{
    public class EnemyController : MonoBehaviour
    {
        public Rigidbody2D rb2d;
        public float moveSpeed;
        public float damage;
        public float health = 5;
        public ItemDropper itemDropper;
        
        public Transform target;
        private float _hitCounter, _knockBackTimer, _hitInterval, _originalMoveSpeed;
        
        
        private void Start()
        {
            _originalMoveSpeed = moveSpeed;
            target = PlayerHealthController.contPHealth.transform;
        }
    
        private void Update()
        {
            rb2d.velocity = (target.position - transform.position).normalized * moveSpeed;
            if (!target) target = PlayerHealthController.contPHealth.transform;
            if (_hitCounter > 0f)
            {
                _hitCounter -= Time.deltaTime;
            }
        
            switch (_knockBackTimer)
            {
                case <= 0:
                    moveSpeed = _originalMoveSpeed;
                    break;
                case > 0f:
                    _knockBackTimer -= Time.deltaTime;
                    break;
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player") && _hitCounter <= 0f)
            {
                PlayerHealthController.contPHealth.TakeDamage(damage);
                _hitCounter = _hitInterval;
            }

            if (collision.gameObject.CompareTag("Construct"))
            {
                collision.gameObject.GetComponentInChildren<Tech>().TakeDamage(damage);
                Debug.Log("got here 1");
                _hitCounter = _hitInterval;
            }
            StopEnemies();
        }

        public void TakeDamage(float enemyDamage)
        {   
            health -= enemyDamage;
            if (health <= 0)
            {
                itemDropper.DropResource();
                gameObject.SetActive(false);
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
        
        private void ShowDamage(float theDamage, float intensity = 1f)
        {
            var floatingText = DamageNumberController.contDmgText
                .player.GetFeedbackOfType<MMF_FloatingText>();
            floatingText.Value = theDamage.ToString();
            if (rb2d) DamageNumberController.contDmgText.player.PlayFeedbacks(transform.position);
        }

        private void StopEnemies()
        {
            if (!PlayerController.contPlayer.gameObject.activeInHierarchy)
            {
                moveSpeed = 0f;
            }
        }
    }
}
