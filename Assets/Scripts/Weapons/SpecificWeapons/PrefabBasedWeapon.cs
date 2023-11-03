using UnityEngine;
using Weapons.WeaponModifiers;

namespace Weapons.SpecificWeapons
{
    public class PrefabBasedWeapon : Weapon
    {
        public BehaviorChecklist it;
        protected float fireInterval, reloadInterval, ammo;
        protected bool canFire = true;
        protected Vector2 direction;
        protected Quaternion rotation;

        void Start()
        {
            SetStats();
        }
    
        private void SetStats()
        {
            fireInterval = stats.weaponLvls[stats.lvl].rateOfFire;
            reloadInterval = stats.weaponLvls[stats.lvl].coolDown;
            ammo = stats.weaponLvls[stats.lvl].ammo;
            var attackRadius = GetComponent<CircleCollider2D>();
            attackRadius.radius = stats.weaponLvls[stats.lvl].range;
        }

        protected virtual void Fire()
        {
            // put logic here
        }
    }
}
