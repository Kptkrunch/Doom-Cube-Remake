using Controllers;
using UnityEngine;

namespace GenUtilsAndTools
{
    public class Projectile : MonoBehaviour
    {
        public EnemyDamager enemyDamager;
        public float moveSpeed;
        public bool doesPenetrate;
        public int numberOfPenetrates;

        private void Update()
        {
            transform.position += transform.up * (moveSpeed * Time.deltaTime);
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
    }
}
