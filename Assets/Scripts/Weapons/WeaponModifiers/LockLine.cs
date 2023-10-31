using UnityEngine;
using Weapons.SpecificWeapons;

namespace Weapons.WeaponModifiers
{
    public class LockLine : MonoBehaviour
    {
        public MissileWeapon parent;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Enemy"))
            {
                var position = collision.transform.position;
                parent.enemy = new Vector2(position.x, position.y);
            }
        }
    }
}
