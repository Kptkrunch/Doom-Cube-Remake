using UnityEngine;
using Weapons.SOS;
using Weapons.WeaponModifiers;

namespace Weapons.SpecificWeapons
{
    public class PrefabBasedWeapon : Weapon
    {
        public BehaviorChecklist it;
        protected float FireInterval, ReloadInterval, Ammo;
        protected bool CanFire = true;
        protected Vector2 Direction;
        protected Quaternion Rotation;

        private void Start()
        {
            SetStats();
        }

        private void SetStats()
        {
            FireInterval = stats.weaponLvls[stats.lvl].rateOfFire;
            ReloadInterval = stats.weaponLvls[stats.lvl].coolDown;
            Ammo = stats.weaponLvls[stats.lvl].ammo;
            var attackRadius = GetComponent<CircleCollider2D>();
        }

        protected virtual void Fire()
        {
            // put logic here
        }
    }
}