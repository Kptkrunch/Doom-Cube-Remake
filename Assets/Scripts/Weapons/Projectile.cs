using Damagers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Weapons
{
    public class Projectile : MonoBehaviour
    {
        public EnemyDamager enemyDamager;
        [HideInInspector]
        public float lobHeight, lobDistance;
        public float moveSpeed, rotationSpeed, lifeTimer, numberOfPenetrates;
        [SerializeField] protected bool doesPenetrate, doesRotate, isLobbed, doesBounce, hasLifetime, useTranslate;
        [SerializeField] protected Rigidbody2D rb2d;
        protected int bounces = 3;
        protected float bounceInterval = 1f;
        protected float bounceTimer = 1f;
        private Vector3 _direction;
        
        private void Update()
        {
            if (useTranslate)
            {
                transform.position += _direction * (moveSpeed * Time.deltaTime);
            }
            
            if (hasLifetime)
            {
                lifeTimer -= Time.deltaTime;
                if (lifeTimer <= 0)
                {
                    gameObject.SetActive(false);
                }
            }
            
            if (!isLobbed)
            {
                var transform1 = transform;
                transform1.position += transform1.up * (moveSpeed * Time.deltaTime);
            }

            if (doesRotate)
            {
                transform.rotation = Quaternion.Euler(0f, 0f,
                    transform.rotation.eulerAngles.z + rotationSpeed * 360f * Time.deltaTime * Mathf.Sign(rb2d.velocity.x));
            }

            switch (doesBounce)
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
                            Destroy(gameObject, bounceTimer * 2);
                        }
                    }

                    break;
                }
            }
        }
        
        protected virtual void OnTriggerEnter2D(Collider2D collision)
        {
            if (doesPenetrate && numberOfPenetrates > 0)
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
            if (isLobbed)
            {
                rb2d.velocity = new Vector2(Random.Range(-lobDistance, lobDistance), lobHeight);
            }
            
            if (doesBounce)
            {
                bounceTimer = Random.Range(bounceTimer * 0.5f, bounceTimer * 1.75f);
            }
        }
        
        public void MoveProjectile(Vector3 direction, bool moveTransform) {
            
            if (moveTransform) rb2d.velocity = direction * moveSpeed;
            if (!moveTransform)
            {
                useTranslate = true;
                _direction = direction;
            }
        }
    }
}
