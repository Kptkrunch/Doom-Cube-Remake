using Controllers;
using UnityEngine;

namespace EffectsTools
{
    public class EnemyDamager : MonoBehaviour
    {
        public float damage;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Enemy"))
            {
                collision.GetComponent<EnemyController>().TakeDamage(damage);
            }
        }
    }
}
