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
        private float _maxHealth, _hitCounter, _knockBackTimer, _hitInterval, _originalMoveSpeed, _lerpTimer;
        private static readonly int FadeAmount = Shader.PropertyToID("_FadeAmount");
        private static readonly int OffsetUvY = Shader.PropertyToID("_OffsetUvY");


        private void Start()
        {
            _maxHealth = health;
            _originalMoveSpeed = moveSpeed;
            target = PlayerHealthController.contPHealth.transform;
            _material = spriteRenderer.material;
        }

        private void FixedUpdate()
        {
            if (!target) target = PlayerHealthController.contPHealth.transform;
            if (_hitCounter > 0f) _hitCounter -= Time.deltaTime;

            FlipRigidBodyX();
            KnockBackTimer();
            CheckDeath();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            MeleeAttack(collision);
            StopEnemies();
        }

        public void TakeDamage(float enemyDamage, string damageType)
        {
            health -= enemyDamage;
            if (health <= 0)
            {
                it.DmgTypeDictionary[damageType] = true;
                health = _maxHealth;
                if (!it.alreadyDropped)
                {
                    it.alreadyDropped = true;
                    itemDropper.DropResource();
                }
            }

            ShowDamage(enemyDamage);
        }

        public void KnockBack(float knockBackAmount, float knockBackDuration)
        {
            _knockBackTimer = knockBackDuration;
            if (_knockBackTimer >= 0) moveSpeed = -moveSpeed * knockBackAmount;
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
                rb2d.transform.localScale = new Vector2(-1, transform.localScale.y);
            else if (rb2d.velocity.x >= 0) rb2d.transform.localScale = new Vector2(1, transform.localScale.y);
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
            if (!PlayerController.contPlayer.gameObject.activeInHierarchy) moveSpeed = 0f;
        }

        private void MeleeAttack(Collision2D collision)
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
                }
                else if (rb2d.velocity.x >= 0)
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
                }
                else if (rb2d.velocity.x >= 0)
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
        }

        private void CheckDeath()
        {
            if (it.DmgTypeDictionary["Deathray"]) DeathrayDeath();

            if (it.DmgTypeDictionary["Acid"]) AcidMeltedDeath();

            if (it.DmgTypeDictionary["Fire"])
            {
                ResetEnemy();
                gameObject.SetActive(false);
            }

            if (it.DmgTypeDictionary["Solid"])
            {
                ResetEnemy();
                gameObject.SetActive(false);
            }

            if (it.DmgTypeDictionary["Mind"])
            {
                ResetEnemy();
                gameObject.SetActive(false);
            }
        }

        private void DeathrayDeath()
        {
            LerpFade();

            if (_material.GetFloat(FadeAmount) >= 1)
            {
                ResetEnemy();
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

            LerpOffSet();

            if (_material.GetFloat(OffsetUvY) >= 1)
            {
                ResetEnemy();
                gameObject.SetActive(false);
            }
        }

        private void LerpFade()
        {
            var amount = Mathf.Lerp(0, 1, _lerpTimer / 1.0f);
            _lerpTimer += Time.deltaTime;
            _material.SetFloat(FadeAmount, amount);
        }

        private void LerpOffSet()
        {
            var amount = Mathf.Lerp(0, 1, _lerpTimer / 1.5f);
            _lerpTimer += Time.deltaTime;
            _material.SetFloat(OffsetUvY, amount);
        }


        private void ResetEnemy()
        {
            _lerpTimer = 0f;
            _material.SetFloat(OffsetUvY, 0);
            moveSpeed = _originalMoveSpeed;
            it.gotDeathParticle = false;
            it.alreadyDropped = false;
            it.DmgTypeDictionary["Deathray"] = false;
            it.DmgTypeDictionary["Acid"] = false;
            it.DmgTypeDictionary["Fire"] = false;
            it.DmgTypeDictionary["Solid"] = false;
            it.DmgTypeDictionary["Energy"] = false;
            it.DmgTypeDictionary["Mind"] = false;
            _material.SetFloat(OffsetUvY, 0);
            _material.SetFloat(FadeAmount, 0.1f);
        }
    }
}