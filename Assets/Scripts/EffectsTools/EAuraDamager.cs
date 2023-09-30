using System.Collections.Generic;
using Controllers;
using UnityEngine;

namespace EffectsTools
{
    public class EAuraDamager : EnemyDamager
    {
        public bool damageOverTime;
        public float damageInterval;
        public float damageTimer;

        public List<EnemyController> enemiesInRadius = new();

        protected override void OnTriggerEnter2D(Collider2D collision)
        {
            base.OnTriggerEnter2D(collision);
            if (damageOverTime)
            {
                if (collision.CompareTag("Enemy"))
                {
                    enemiesInRadius.Add(collision.GetComponent<EnemyController>());
                }
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (damageOverTime)
            {
                if (collision.CompareTag("Enemy"))
                {
                    enemiesInRadius.Remove(collision.GetComponent<EnemyController>());
                }
            }
        }
    }
}
