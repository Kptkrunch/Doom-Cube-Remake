using System.Collections.Generic;
using Controllers;
using UnityEngine;

namespace Weapons
{
    public class Weapon : MonoBehaviour
    {
        public List<WeaponStats> stats;
        public int weaponLevel;
        public Sprite weaponIcon;
        
        public void WeaponLevelUp()
        {
            if (weaponLevel < stats.Count - 1)
            {
                weaponLevel++;

                if (weaponLevel >= stats.Count - 1)
                {
                    WeaponController.contWeps.fullyUpgradedWeapons.Add(this);
                    WeaponController.contWeps.equippedWeapons.Remove(this);
                }
            }
        }

        public virtual void UpdateWeapon()
        {
            WeaponLevelUp();
            
            stats[weaponLevel].projSpeed *= stats[weaponLevel].projSpeed;
            stats[weaponLevel].range *= stats[weaponLevel].range;
            stats[weaponLevel].rateOfFire *= stats[weaponLevel].rateOfFire;
            stats[weaponLevel].duration *= stats[weaponLevel].duration;
            stats[weaponLevel].size *= stats[weaponLevel].size;
            stats[weaponLevel].cdr *= stats[weaponLevel].cdr;
        }
    }

    [System.Serializable]
    public class WeaponStats
    {
        public float projSpeed, damage, range, rateOfFire, numOfProj, duration, size, cdr;
        public string name, description, upgradeText;
    }
}