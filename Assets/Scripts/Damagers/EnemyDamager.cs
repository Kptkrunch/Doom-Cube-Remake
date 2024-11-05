using Controllers;
using Objects;
using UnityEngine;

namespace Damagers
{
    public class EnemyDamager : MonoBehaviour
    {
        public float damage;
        public string damageType;
        public bool destructive, hitsGround, hitsAir, fireDamage, chemicalDamage, explosiveDamage;

        protected virtual void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Enemy")) collision.GetComponent<EnemyController>().TakeDamage(damage, damageType);

            if (collision.CompareTag("BasicObject") && destructive)
                collision.GetComponent<BasicObject>().TakeDamage(damage, damageType);
        }
    }
}