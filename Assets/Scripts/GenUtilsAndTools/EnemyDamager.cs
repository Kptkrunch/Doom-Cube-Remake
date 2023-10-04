using Controllers;
using UnityEngine;

namespace GenUtilsAndTools
{
    public class EnemyDamager : MonoBehaviour
    {
        public float damage;
        public bool destructive, hitsGround, hitsAir, fireDamage, chemicalDamage, explosiveDamage;

        protected virtual void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Enemy"))
            {
                collision.GetComponent<EnemyController>().TakeDamage(damage);
            }

            if (collision.CompareTag("WorldlyObject"))
            {
                collision.GetComponent<WordlyObject>().TakeDamage(damage);
            }
        }
    }
}
