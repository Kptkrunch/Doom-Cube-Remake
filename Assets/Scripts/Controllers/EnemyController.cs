using Controllers.Pools;
using GenUtilsAndTools;
using MoreMountains.Feedbacks;
using TechSkills;
using UnityEngine;

namespace Controllers
{
    public class EnemyController : MonoBehaviour
    {
        public Rigidbody2D rb2d;
        public GameObject attackLocation;
        public SpriteRenderer spriteRenderer;
        public EnemyBools it;
        public int attackIndex;
        public float moveSpeed;
        public float damage;
        public float health = 5;
        public ItemDropper itemDropper;
        public Transform target;

        private Material _material;
        private float _hitCounter, _knockBackTimer, _hitInterval, _originalMoveSpeed, _lerpTimer;
        private static readonly int FadeAmount = Shader.PropertyToID("_FadeAmount");
        private static readonly int OffsetUvY = Shader.PropertyToID("_OffsetUvY");


        private void Start()
        {
            _originalMoveSpeed = moveSpeed;
            target = PlayerHealthController.contPHealth.transform;
            _material = spriteRenderer.material;
        }
    
        private void Update()
        {
            if (!target) target = PlayerHealthController.contPHealth.transform;
            
            if (it.deathRay)
            {
                DeathRayDeath();
            }

            if (it.melting)
            {
                AcidMeltedDeath();
            }
            
            FlipRigidBodyX();
            
            if (_hitCounter > 0f)
            {
                _hitCounter -= Time.deltaTime;
            }
            KnockBackTimer();

        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player") && _hitCounter <= 0f)
            {
                var attack = EnemyAttackPools.PoolEnemyAtk.attackList[attackIndex].GetPooledGameObject();
                attack.transform.position = attackLocation.transform.position;
                if (rb2d.velocity.x < 0)
                {
                    var localScale = attack.transform.localScale;
                    localScale =
                        new Vector3(1, localScale.y, localScale.z);
                    attack.transform.localScale = localScale;
                } else if (rb2d.velocity.x >= 0)
                {
                    var localScale = attack.transform.localScale;
                    localScale =
                        new Vector3(-1, localScale.y, localScale.z);
                    attack.transform.localScale = localScale;
                }
                attack.SetActive(true);
                PlayerHealthController.contPHealth.TakeDamage(damage);
                _hitCounter = _hitInterval;
            }

            if (collision.gameObject.CompareTag("Construct"))
            {
                var attack = EnemyAttackPools.PoolEnemyAtk.attackList[attackIndex].GetPooledGameObject();
                attack.transform.position = attackLocation.transform.position;
                if (rb2d.velocity.x < 0)
                {
                    var localScale = attack.transform.localScale;
                    localScale =
                        new Vector3(1, localScale.y, localScale.z);
                    attack.transform.localScale = localScale;
                } else if (rb2d.velocity.x >= 0)
                {
                    var localScale = attack.transform.localScale;
                    localScale =
                        new Vector3(-1, localScale.y, localScale.z);
                    attack.transform.localScale = localScale;
                }
                attack.SetActive(true);
                collision.gameObject.GetComponentInChildren<Tech>().TakeDamage(damage);
                _hitCounter = _hitInterval;
            }
            StopEnemies();
        }

        public void TakeDamage(float enemyDamage)
        {   
            health -= enemyDamage;
            if (health <= 0)
            {
                // it.deathRay = true;
                it.melting = true;
                itemDropper.DropResource();
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

        private void FlipRigidBodyX()
        {
            rb2d.velocity = (target.position - transform.position).normalized * moveSpeed;
            if (rb2d.velocity.x < 0)
            {
                rb2d.transform.localScale = new Vector2(-1, transform.localScale.y);
            } else if (rb2d.velocity.x >= 0)
            {
                rb2d.transform.localScale = new Vector2(1, transform.localScale.y);
            }
        }

        private void KnockBackTimer()
        {
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

        private void StopEnemies()
        {
            if (!PlayerController.contPlayer.gameObject.activeInHierarchy)
            {
                moveSpeed = 0f;
            }
        }

        private void DeathRayDeath()
        {
            var amount = Mathf.Lerp(0, 1, _lerpTimer / 1.0f);
            _lerpTimer += Time.deltaTime;
            _material.SetFloat(FadeAmount, amount);
            if (_material.GetFloat(FadeAmount) >= 1)
            {
                _material.SetFloat(FadeAmount, 0);
                _lerpTimer = 0;
                gameObject.SetActive(false);
            }
        }

        private void AcidMeltedDeath()
        {
            moveSpeed = 0;
            if (!it.gotDeathParticle)
            {
                it.gotDeathParticle = true;
                var acid = EnemyDeathPoolManager.PoolEnemyMan.meltingPool.GetPooledGameObject();
                var position = transform.position;
                acid.transform.position = new Vector3(position.x, position.y - .35f, position.z);
                acid.SetActive(true);
            }

            var amount = Mathf.Lerp(0, 1, _lerpTimer / 1.5f);
            _lerpTimer += Time.deltaTime;
            _material.SetFloat(OffsetUvY, amount);
            
            if (_material.GetFloat(OffsetUvY) >= 1)
            {
                _material.SetFloat(OffsetUvY, 0);
                _lerpTimer = 0;
                moveSpeed = _originalMoveSpeed;
                it.gotDeathParticle = false;
                gameObject.SetActive(false);
            }
                
        }
    }
}
