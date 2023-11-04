using System;
using Damagers;
using JetBrains.Annotations;
using UnityEngine;
using Weapons.WeaponModifiers;
using Random = UnityEngine.Random;

namespace Weapons.Projectiles
{
    public class Projectile : MonoBehaviour
    {
        public BehaviorChecklist it;
        public ProjectileData pd;
        public GameObject parent;
        [CanBeNull] public EnemyDamager enemyDamager;
        private float _lifeTime;
        private void Awake()
        {
            pd = Instantiate(pd);
            _lifeTime = pd.stats.lifeTime;
        }

        private void FixedUpdate()
        {
            _lifeTime -= Time.deltaTime;

            MaybeMoveViaTranslation();
            MaybeHasLifetime();
        }

        protected virtual void OnTriggerEnter2D(Collider2D collision)
        {
            PenetrateLogic(collision);
            MaybeDisableOnContactWithAnything(collision);
        }

        public virtual void OnEnable()
        {
            MaybeMoveRandomDirection();
        }
        
        private void PenetrateLogic(Collider2D collision)
        {
            if (it.doesPenetrate && pd.stats.pens > 0)
            {
                if (collision.CompareTag("Enemy"))
                {
                    pd.stats.pens--;
                    if (pd.stats.pens <= 0)
                    {
                        gameObject.SetActive(false);
                    }
                } else if (collision.CompareTag("Enemy") && !collision.CompareTag("Player"))
                {
                    gameObject.SetActive(false);
                }
            }
        }
        protected void MaybeMoveViaTranslation()
        {
            if (it.moveUseTranslate)
            {
                transform.Translate(pd.stats.direction * (pd.stats.movSpeed * Time.deltaTime));
            }
        }

        protected void MaybeRotate(Rigidbody2D rb2d)
        {
            if (it.doesRotate)
            {
                transform.rotation = Quaternion.Euler(0f, 0f,
                    transform.rotation.eulerAngles.z + pd.stats.rotSpeed * 360f * Time.deltaTime * Mathf.Sign(rb2d.velocity.x));
            }
        }

        protected void MaybeHasLifetime()
        {
            if (_lifeTime <= 0)
            {
                _lifeTime = pd.stats.lifeTime;
                parent.gameObject.SetActive(false);
            }
        }

        protected void MaybeDisableOnContactWithAnything(Collider2D collision)
        {
            if (collision.CompareTag("Enemy") 
                || collision.CompareTag("WorldlyObject") 
                && !it.doesPenetrate && it.disableOnContact
                && !collision.CompareTag("Player")) parent.gameObject.SetActive(false);
        }

        protected void MaybeMoveBackwards()
        {
            if (it.movesBackwards && it.moveUseVelocity)
            {
                Debug.Log(pd.stats.direction);
                pd.stats.direction = new Vector2(pd.stats.direction.x, pd.stats.direction.y) * -1;
                Debug.Log(pd.stats.direction);
            }
        }

        protected void MaybeMoveRandomDirection()
        {
            if (it.randomDirection)
            {
                pd.stats.direction = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)).normalized;
                var angle = Mathf.Atan2(pd.stats.direction.x, pd.stats.direction.y) * Mathf.Deg2Rad - 90;
                var rotation = Quaternion.Euler(0f, 0f, angle).normalized;
                transform.rotation = rotation;
            }
        }
    }
}
