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
        [CanBeNull] public EnemyDamager enemyDamager;
        [HideInInspector]
        public float lobHeight, lobDistance;
        public float moveSpeed, rotationSpeed, lifeTimer, numberOfPenetrates;
        [SerializeField] protected Rigidbody2D rb2d;
        protected int bounces = 3;
        protected float hardBoundeInterval = 1f;
        protected float bounceInterval = 1f;
        protected float bounceTimer = 1f;
        protected Vector3 direction;
        
        private void Update()
        {
            if (it.useTranslate)
            {
                transform.position += direction * (moveSpeed * Time.deltaTime);
            }
            
            if (it.hasLifetime)
            {
                lifeTimer -= Time.deltaTime;
                if (lifeTimer <= 0)
                {
                    gameObject.SetActive(false);
                }
            }
            
            if (!it.isLobbed)
            {
                var transform1 = transform;
                transform1.position += transform1.up * (moveSpeed * Time.deltaTime);
            }

            if (it.doesRotate)
            {
                transform.rotation = Quaternion.Euler(0f, 0f,
                    transform.rotation.eulerAngles.z + rotationSpeed * 360f * Time.deltaTime * Mathf.Sign(rb2d.velocity.x));
            }

            switch (it.doesBounce)
            {
                case true:
                {
                    bounceTimer -= Time.deltaTime;
                    if (bounceTimer <= 0)
                    {
                        var velocity = rb2d.velocity;
                        velocity = new Vector2(velocity.x, velocity.y).normalized;
                        rb2d.velocity = velocity;
                        bounceInterval *= 0.8f;
                        bounceTimer = bounceInterval;
                        rb2d.AddForce(new Vector2(velocity.x * 0.8f, lobHeight * .75f), ForceMode2D.Impulse);
                        bounces--;
            
                        if (bounces <= 0)
                        {
                            rb2d.velocity = Vector2.zero;
                            rb2d.gravityScale = 0f;
                            if (it.disableAfterBounces) gameObject.SetActive(false);
                            it.delayDisable = true;
                        }
                    }
            
                    break;
                }
            }
        }
        
        protected virtual void OnTriggerEnter2D(Collider2D collision)
        {
            if (it.doesPenetrate && numberOfPenetrates > 0)
            {
                if (collision.CompareTag("Enemy"))
                {
                    numberOfPenetrates--;
                    if (numberOfPenetrates <= 0)
                    {
                        gameObject.SetActive(false);
                    }
                } else if (collision.CompareTag("Enemy") && !collision.CompareTag("Player"))
                {
                    gameObject.SetActive(false);
                }
            }
        }

        private void OnEnable()
        {
            if (it.isLobbed)
            {
                rb2d.gravityScale = 1;
                rb2d.velocity = new Vector2(Random.Range(-lobDistance, lobDistance), lobHeight + 1);
            }
            
            if (it.doesBounce)
            {
                bounceTimer = Random.Range(bounceTimer * 0.5f, bounceTimer * 1.75f);
            }
        }
        
        public void MoveProjectile(Vector3 dir, bool moveTransform) {
            
            if (moveTransform) rb2d.velocity = dir * (moveSpeed * Time.deltaTime);
            if (!moveTransform)
            {
                it.useTranslate = true;
                direction = dir;
            }
        }
    }
}
