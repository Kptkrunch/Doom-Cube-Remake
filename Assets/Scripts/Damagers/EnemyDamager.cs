using Controllers;
using Objects;
using UnityEngine;

namespace Damagers
{
    public class EnemyDamager : MonoBehaviour
    {
        public float damage;
        public string damageType;
        public bool destructive, antiGround, antiAir, fire, acid, explosive, energy, deathray, solid;
        public bool[] damageTypeArray;
        
        private void Awake()
        {
            damageTypeArray = new[]
                { fire, acid, explosive, energy, deathray, solid };

            if (damageTypeArray.Length.Equals(0)) return; 
            foreach (var dt in damageTypeArray)
            {
                if (dt)
                {
                    damageType = nameof(dt);
                    return;
                }
            }
        }

        protected virtual void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Enemy")) collision.GetComponent<EnemyController>().TakeDamage(damage, damageType);

            if (collision.CompareTag("BasicObject") && destructive)
                collision.GetComponent<BasicObject>().TakeDamage(damage, damageType);
        }
    }
}