using Controllers;
using GenUtilsAndTools;
using Objects;
using UnityEngine;

namespace Damagers
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

            if (destructive && collision.CompareTag("WorldlyObject"))
            {
                collision.GetComponent<WordlyObject>().TakeDamage(damage);
            }
        }
    }
}