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
        private float _lifeTime, _pens;

        private void Awake()
        {
            pd = Instantiate(pd);
            _lifeTime = pd.stats.lifeTime;
            _pens = pd.stats.pens;
        }

        private void FixedUpdate()
        {
            if (it.hasLifetime) _lifeTime -= Time.deltaTime;

            MaybeMoveViaTranslation();
            MaybeHasLifetime();
            MaybeMoveTowards(pd.stats.direction);
        }

        protected virtual void OnTriggerEnter2D(Collider2D collision)
        {
            if (it.doesPenetrate) PenetrateLogic(collision);
            if (it.disableOnContact) MaybeDisableOnContactWithAnything(collision);
        }

        public virtual void OnEnable()
        {
            MaybeMoveRandomDirection();
        }

        private void PenetrateLogic(Collider2D collision)
        {
            if (collision.CompareTag("Enemy"))
            {
                _pens--;
                if (_pens <= 0)
                {
                    _pens = pd.stats.pens;
                    gameObject.SetActive(false);
                }
            }
        }

        protected void MaybeMoveViaTranslation()
        {
            if (it.moveUseTranslate) transform.Translate(pd.stats.direction * (pd.stats.movSpeed * Time.deltaTime));
        }

        protected void MaybeMoveTowards(Vector2 target)
        {
            if (it.moveUseMoveTowards) Vector2.MoveTowards(transform.position, target, pd.stats.movSpeed);
        }

        protected void MaybeRotate(Rigidbody2D rb2d)
        {
            if (it.doesRotate)
                transform.rotation = Quaternion.Euler(0f, 0f,
                    transform.rotation.eulerAngles.z +
                    pd.stats.rotSpeed * 360f * Time.deltaTime * Mathf.Sign(rb2d.velocity.x));
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
            if (collision.CompareTag("Enemy") || collision.CompareTag("WorldlyObject"))
                parent.gameObject.SetActive(false);
        }

        protected void MaybeMoveBackwards()
        {
            if (it.movesBackwards && it.moveUseVelocity)
                pd.stats.direction = new Vector2(pd.stats.direction.x, pd.stats.direction.y) * -1;
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

        private void OnDisable()
        {
            _lifeTime = pd.stats.lifeTime;
            _pens = pd.stats.pens;
        }
    }
}