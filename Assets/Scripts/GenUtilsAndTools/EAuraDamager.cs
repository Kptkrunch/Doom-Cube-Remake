using System.Collections.Generic;
using Controllers;
using UnityEngine;

namespace GenUtilsAndTools
{
    public class EAuraDamager : EnemyDamager
    {
        public bool damageOverTime;
        public float damageInterval;
        public float damageTimer;
        public float auraDamage;

        private List<EnemyController> _enemiesInRadius = new();

        private void Update()
        {
            if (damageOverTime)
            {
                damageTimer -= Time.deltaTime;
                if (damageTimer <= 0)
                {
                    damageTimer = damageInterval;

                    for (var i = 0; i < _enemiesInRadius.Count; i++)
                    {
                        if (_enemiesInRadius[i])
                        {
                            _enemiesInRadius[i].TakeDamage(auraDamage);
                        }
                        else
                        {
                            _enemiesInRadius.RemoveAt(i);
                            i--;
                        }
                    }
                }
            }
        }

        protected override void OnTriggerEnter2D(Collider2D collision)
        {
            base.OnTriggerEnter2D(collision);
            if (!damageOverTime)
            {
                if (collision.CompareTag("Enemy"))
                {
                    collision.GetComponent<EnemyController>().TakeDamage(auraDamage);
                }
            }
            else
            {
                if (collision.CompareTag("Enemy"))
                {
                    _enemiesInRadius.Add(collision.GetComponent<EnemyController>());
                }
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (damageOverTime)
            {
                if (collision.CompareTag("Enemy"))
                {
                    _enemiesInRadius.Remove(collision.GetComponent<EnemyController>());
                }
            }
        }
    }
}
