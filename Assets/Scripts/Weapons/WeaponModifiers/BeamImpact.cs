using Controllers;
using UnityEngine;

namespace Weapons.WeaponModifiers
{
    public class BeamImpact : MonoBehaviour
    {
        public Weapon parent;
        private float _damageTimer, _damageInterval, _damage;

        private void Start()
        {
            _damageInterval = parent.stats[parent.weaponLevel].rateOfFire;
            _damageTimer = _damageInterval;
            _damage = parent.stats[parent.weaponLevel].damage;
        }
        private void FixedUpdate()
        {
            _damageTimer -= Time.deltaTime;
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
        
            if (collision.CompareTag("Enemy"))
            {
                Debug.Log("Hit");
                if (_damageTimer <= 0)
                {
                    Debug.Log("Damage?");
                    _damageTimer = _damageInterval;
                    collision.GetComponent<EnemyController>().TakeDamage(_damage);
                }
            }
        }
    }
}
