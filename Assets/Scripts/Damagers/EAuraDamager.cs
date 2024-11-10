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
        private readonly List<BasicObject> _objectsInRadius = new();

        private void FixedUpdate()
        {
            switch (damageOverTime)
            {
                case false:
                    return;
            }

            _damageTimer -= Time.deltaTime;
            if (!(_damageTimer <= 0)) return;
            _damageTimer = damageInterval;

            for (var i = 0; i < _enemiesInRadius.Count; i++)
            {
                if (!_enemiesInRadius[i].isActiveAndEnabled)
                {
                    _enemiesInRadius.RemoveAt(i);
                    i--;
                }

                if (_enemiesInRadius[i])
                {
                    _enemiesInRadius[i].TakeDamage(damage, damageType);
                }
                else
                {
                    _enemiesInRadius.RemoveAt(i);
                    i--;
                }
            }

            for (var i = 0; i < _objectsInRadius.Count; i++)
            {
                if (!_objectsInRadius[i].isActiveAndEnabled)
                {
                    _objectsInRadius.RemoveAt(i);
                    i--;
                }

                if (_objectsInRadius[i])
                {
                    _objectsInRadius[i].TakeDamage(damage, damageType);
                }
                else
                {
                    _objectsInRadius.RemoveAt(i);
                    i--;
                }
            }
        }

        protected override void OnTriggerEnter2D(Collider2D collision)
        {
            base.OnTriggerEnter2D(collision);
            if (!damageOverTime)
            {
                if (collision.CompareTag("Enemy"))
                    collision.GetComponent<EnemyController>().TakeDamage(damage, damageType);

                if (collision.CompareTag("BasicObject")) collision.GetComponent<BasicObject>().TakeDamage(damage, damageType);
            }
            else
            {
                if (collision.CompareTag("Enemy")) _enemiesInRadius.Add(collision.GetComponent<EnemyController>());

                if (collision.CompareTag("BasicObject")) _objectsInRadius.Add(collision.GetComponent<BasicObject>());
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            switch (damageOverTime)
            {
                case true:
                {
                    if (collision.CompareTag("Enemy"))
                        _enemiesInRadius.Remove(collision.GetComponent<EnemyController>());

                    if (collision.CompareTag("BasicObject"))
                        _objectsInRadius.Remove(collision.GetComponent<BasicObject>());

                    break;
                }
            }
        }
    }
}