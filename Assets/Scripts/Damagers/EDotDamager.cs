using Controllers;
using UnityEngine;

namespace Damagers
{
    public class EDotDamager : EnemyDamager
    {
        public float damageInterval;
        
        private float _damageTimer;


        private void Start()
        {
            SetStats();
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            _damageTimer -= Time.deltaTime;

            if (_damageTimer <= 0)
            {
                _damageTimer = damageInterval;
                if (collision.CompareTag("Enemy"))
                {
                    collision.GetComponent<EnemyController>().TakeDamage(damage);
                }
            }
        }

        private void SetStats()
        {
            _damageTimer = damageInterval;
        }
    }
}
