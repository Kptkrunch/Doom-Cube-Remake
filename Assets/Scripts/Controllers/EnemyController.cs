using System;
using Controllers.Pools;
using EnemyStuff;
using GenUtilsAndTools;
using MoreMountains.Feedbacks;
using ScriptableObjs;
using TechSkills;
using UnityEngine;

namespace Controllers
{
    public class EnemyController : MonoBehaviour
    {
        public string id;
        public Rigidbody2D rb2d;
        public GameObject attackLocation;
        public SpriteRenderer spriteRenderer;
        public EnemyBools it;
        public ItemDropper itemDropper;
        public Transform target;

        private Material _material;
        private static readonly int FadeAmount = Shader.PropertyToID("_FadeAmount");
        private static readonly int OffsetUvY = Shader.PropertyToID("_OffsetUvY");
        
        private EnemyDatabase.EnemyData _stats;
        
        private float _health = 1, _moveSpeed, _damage, _rateOfFire, _range, _knockBackTimer = 0.0f, _lerpTimer;
        private int _collisionAttackIndex, _rangedAttackIndex, _specialAttackIndex, _ammo;
        
        
        private void Awake()
        {
            if (EnemyDataManager.Instance.enemyDb) _stats = EnemyDataManager.Instance.enemyDb.GetEnemyData(id);
        }

        private void Start()
        {
            InitEnemyStats();
            if (!target) target = PlayerHealthController.contPHealth.transform;
            _material = Instantiate(spriteRenderer.material);
        }

        private void FixedUpdate()
        {
            if (_rateOfFire > 0f) _rateOfFire -= Time.deltaTime;

            FlipRigidBodyX();
            // KnockBackTimer();
            CheckDeath();
        }

        // private void OnCollisionEnter2D(Collision2D collision)
        // {
        //     if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Construct"))
        //     {
        //         CollisionAttack(collision);
        //         StopEnemies();
        //     }
        // }

        private void OnEnable()
        {
            if (!target) target = PlayerHealthController.contPHealth.transform;
            InitEnemyStats();
        }

        public void TakeDamage(float enemyDamage, string damageType)
        {
            _health -= enemyDamage;
            if (_health <= 0)
            {
                it.DmgTypeDictionary[damageType] = true;

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
            if (_knockBackTimer >= 0) _moveSpeed = -_moveSpeed * knockBackAmount;
        }

        private void ShowDamage(float theDamage, float intensity = 1f)
        {
            var floatingText = DamageNumberController.Instance
                .player.GetFeedbackOfType<MMF_FloatingText>();
            floatingText.Value = theDamage.ToString();
            if (rb2d) DamageNumberController.Instance.player.PlayFeedbacks(transform.position);
        }

        private void FlipRigidBodyX()
        {
            rb2d.velocity = (target.position - transform.position).normalized * _moveSpeed;
            if (rb2d.velocity.x < 0)
                rb2d.transform.localScale = new Vector2(-1, transform.localScale.y);
            else if (rb2d.velocity.x >= 0) rb2d.transform.localScale = new Vector2(1, transform.localScale.y);
        }

        private void KnockBackTimer()
        {
            switch (_knockBackTimer)
            {
                case <= 0:
                    _moveSpeed = _stats.moveSpeed;
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
                _moveSpeed = 0f;
            }
        }

        private void CollisionAttack(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player") && _rateOfFire <= 0f)
            {
                var attack = EnemyAttackPools.PoolEnemyAtk.attackList[_collisionAttackIndex].GetPooledGameObject();
                attack.transform.position = attackLocation.transform.position;
                switch (rb2d.velocity.x)
                {
                    case < 0:
                    {
                        var localScale = attack.transform.localScale;
                        localScale =
                            new Vector3(1, localScale.y, localScale.z);
                        attack.transform.localScale = localScale;
                        break;
                    }
                    case >= 0:
                    {
                        var localScale = attack.transform.localScale;
                        localScale =
                            new Vector3(-1, localScale.y, localScale.z);
                        attack.transform.localScale = localScale;
                        break;
                    }
                }

                attack.SetActive(true);
                PlayerHealthController.contPHealth.TakeDamage(_damage);
                _rateOfFire = _stats.rateOfFire;
            }

            if (collision.gameObject.CompareTag("Construct"))
            {
                var attack = EnemyAttackPools.PoolEnemyAtk.attackList[_collisionAttackIndex].GetPooledGameObject();
                attack.transform.position = attackLocation.transform.position;
                switch (rb2d.velocity.x)
                {
                    case < 0:
                    {
                        var localScale = attack.transform.localScale;
                        localScale =
                            new Vector3(1, localScale.y, localScale.z);
                        attack.transform.localScale = localScale;
                        break;
                    }
                    case >= 0:
                    {
                        var localScale = attack.transform.localScale;
                        localScale =
                            new Vector3(-1, localScale.y, localScale.z);
                        attack.transform.localScale = localScale;
                        break;
                    }
                }

                attack.SetActive(true);
                collision.gameObject.GetComponentInChildren<Tech>().TakeDamage(_damage);
                _rateOfFire = _stats.rateOfFire;
            }
        }

        private void CheckDeath()
        {
            if (_health > 0) return;
            
            if (it.DmgTypeDictionary["Deathray"]) DeathrayDeath();

            if (it.DmgTypeDictionary["Acid"]) AcidMeltedDeath();

            if (it.DmgTypeDictionary["Fire"])
            {
                ResetEnemyFloats();
                gameObject.SetActive(false);
            }

            if (it.DmgTypeDictionary["Solid"])
            {
                ResetEnemyFloats();
                gameObject.SetActive(false);
            }

            if (it.DmgTypeDictionary["Energy"])
            { 
                ResetEnemyFloats();
                gameObject.SetActive(false);
            }
        }

        private void DeathrayDeath()
        {
            LerpFade();

            if (_material.GetFloat(FadeAmount) >= 1)
            {
                ResetEnemyFloats();
                InitEnemyStats();
                gameObject.SetActive(false);
            }
        }

        private void AcidMeltedDeath()
        {
            _moveSpeed = 0;
            if (!it.gotDeathParticle)
            {
                it.gotDeathParticle = true;
                var acid = EnemyDeathPoolManager.PoolEnemyDeathPoolManager.meltingPool.GetPooledGameObject();
                var position = transform.position;
                acid.transform.position = new Vector3(position.x, position.y - .35f, position.z);
                acid.SetActive(true);
            }

            LerpOffSet();

            if (_material.GetFloat(OffsetUvY) >= 1)
            {
                ResetEnemyFloats();
                InitEnemyStats();
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

        private void ResetEnemyFloats()
        {
            _moveSpeed = _stats.moveSpeed;
            _lerpTimer = 0f;
            _material.SetFloat(OffsetUvY, 0);
            _material.SetFloat(OffsetUvY, 0);
            _material.SetFloat(FadeAmount, 0.1f);
        }
        
        private void InitEnemyStats() {
            _ammo = _stats.ammo;
            _damage = _stats.damage;
            _health = _stats.health;
            _ammo = _stats.ammo;
            _rateOfFire = _stats.rateOfFire;
            _range = _stats.range;
            _moveSpeed = _stats.moveSpeed;

            _collisionAttackIndex = _stats.collisionAttackIndex;
            _rangedAttackIndex = _stats.rangedAttackIndex;
            _specialAttackIndex = _stats.specialAttackIndex;
        }
    }
}