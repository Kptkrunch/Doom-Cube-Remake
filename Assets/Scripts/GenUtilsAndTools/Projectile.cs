using UnityEngine;
using Random = UnityEngine.Random;

namespace GenUtilsAndTools
{
    public class Projectile : MonoBehaviour
    {
        public EnemyDamager enemyDamager;
        [HideInInspector]
        public float moveSpeed, rotationSpeed, lifeTimer, lobHeight, lobDistance;
        public bool doesPenetrate, doesRotate, isLobbed, hasLifetime;
        public int numberOfPenetrates;
        [SerializeField] private Rigidbody2D rb2d;

        private void Update()
        {
            if (!isLobbed)
            {
                var transform1 = transform;
                transform1.position += transform1.up * (moveSpeed * Time.deltaTime);
            }

            if (doesRotate)
            {
                transform.rotation = Quaternion.Euler(0f, 0f,
                    transform.rotation.eulerAngles.z + (rotationSpeed * 360f * Time.deltaTime * Mathf.Sign(rb2d.velocity.x)));
            }
        }
        
        protected virtual void OnTriggerEnter2D(Collider2D collision)
        {
            if (doesPenetrate)
            {
                if (collision.CompareTag("Enemy"))
                {
                    if (numberOfPenetrates <= 0)
                    {
                        Destroy(gameObject);
                    }
                }
            }
        }

        protected void OnTriggerExit2D(Collider2D other)
        {
            numberOfPenetrates--;
        }

        private void OnEnable()
        {
            if (hasLifetime)
            {
                if (isLobbed)
                {
                    rb2d.velocity = new Vector2(Random.Range(-lobDistance, lobDistance), lobHeight);
                }
                Destroy(gameObject, lifeTimer);
            }
        }
    }
}
