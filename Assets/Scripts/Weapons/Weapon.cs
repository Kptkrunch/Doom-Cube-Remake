using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{
    public class Weapon : MonoBehaviour
    {
        public List<WeaponStats> stats;
        public int weaponLevel;
        public bool weaponLeveledUp = false;
        public Sprite weaponIcon;
        public void WeaponLevelUp()
        {
            if (weaponLevel < stats.Count - 1)
            {
                weaponLevel++;
                weaponLeveledUp = true;

                if (weaponLevel >= stats.Count - 1)
                {
                    WepsAndAbs.wepsAndAbs.fullyUpgradedWeapons.Add(this);
                    WepsAndAbs.wepsAndAbs.equippedWeapons.Remove(this);
                }
            }
        }
    }

    [System.Serializable]
    public class WeaponStats
    {
        public float projSpeed, damage, range, rateOfFire, numOfProj, duration, size, cdr;
        public string name, description, upgradeText;
    }
}