using System.Collections.Generic;
using Controllers;
using Objects;
using UnityEngine;

namespace Damagers
{
    public class EAuraDamager : EnemyDamager
    {
        public bool damageOverTime;
        public float damageInterval;
        
        private float _damageTimer;
        private readonly List<EnemyController> _enemiesInRadius = new();
        private readonly List<WordlyObject> _objectsInRadius = new();

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
                    
                    for (var i = 0; i < _objectsInRadius.Count; i++)
                    {
                        if ( _objectsInRadius[i])
                        {
                            _objectsInRadius[i].TakeDamage(damage);
                        }
                        else
                        {
                            _objectsInRadius.RemoveAt(i);
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

                if (collision.CompareTag("WorldlyObject"))
                {
                    collision.GetComponent<WordlyObject>().TakeDamage(damage);
                }
            }
            else
            {
                if (collision.CompareTag("Enemy"))
                {
                    _enemiesInRadius.Add(collision.GetComponent<EnemyController>());
                }
                
                if (collision.CompareTag("WorldlyObject"))
                {
                    _objectsInRadius.Add(collision.GetComponent<WordlyObject>());
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

                if (collision.CompareTag("WorldlyObject"))
                {
                    _objectsInRadius.Remove(collision.GetComponent<WordlyObject>());
                }
            }
        }
    }
}