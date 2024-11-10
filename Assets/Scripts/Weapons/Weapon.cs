using System;
using Controllers;
using UnityEngine;
using Weapons.SOS;

namespace Weapons
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] public WeaponData stats;
        public Sprite uiSprite;

        private void Awake()
        {
            stats = Instantiate(stats);
        }

        public void WeaponLevelUp()
        {
            if (stats.lvl < stats.weaponLvls.Count - 1)
            {
                stats.lvl++;

                if (stats.lvl >= stats.weaponLvls.Count - 1)
                {
                    WeaponController.contWeps.fullyUpgradedWeapons.Add(this);
                    WeaponController.contWeps.equippedWeapons.Remove(this);
                }
            }
        }

        public virtual void UpdateWeapon()
        {
            WeaponLevelUp();
            stats.weaponLvls[stats.lvl].range = stats.weaponLvls[stats.lvl].range;
            stats.weaponLvls[stats.lvl].rateOfFire = stats.weaponLvls[stats.lvl].rateOfFire;
            stats.weaponLvls[stats.lvl].duration = stats.weaponLvls[stats.lvl].duration;
            stats.weaponLvls[stats.lvl].coolDown = stats.weaponLvls[stats.lvl].coolDown;
            stats.weaponLvls[stats.lvl].ammo = stats.weaponLvls[stats.lvl].ammo;
        }
    }
}