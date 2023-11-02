using Controllers;
using UnityEngine;

namespace Weapons.WeaponModifiers
{
    public class WeaponKnockback : MonoBehaviour
    {
        public float knockBackAmount;
        public float knockBackDuration;
    
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Enemy"))
            {
                collision.GetComponent<EnemyController>().KnockBack(knockBackAmount, knockBackDuration);
            }
        }
    }
}

