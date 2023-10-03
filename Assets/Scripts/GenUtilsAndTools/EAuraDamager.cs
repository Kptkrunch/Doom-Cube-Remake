using System.Collections.Generic;
using Controllers;
using UnityEngine;

namespace GenUtilsAndTools
{
    public class EAuraDamager : EnemyDamager
    {
        public bool damageOverTime;
        public float damageInterval;
        
        private float _damageTimer;
        private List<EnemyController> _enemiesInRadius = new();

        private void Update()
        {
            if (damageOverTime)
            {
                _damageTimer -= Time.deltaTime;
                if (_damageTimer <= 0)
                {
                    _damageTimer = damageInterval;

                    for (var i = 0; i < _enemiesInRadius.Count; i++)
                    {
                        if (_enemiesInRadius[i])
                        {
                            _enemiesInRadius[i].TakeDamage(damage);
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
                    collision.GetComponent<EnemyController>().TakeDamage(damage);
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
