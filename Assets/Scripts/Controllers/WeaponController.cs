using System.Collections.Generic;
using UnityEngine;
using Weapons;

namespace Controllers
{
    public class WeaponController : MonoBehaviour
    {
        public static WeaponController contWeps;
        public List<Weapon> equippedWeapons, upgradableWeapons, allWeapons, fullyUpgradedWeapons;
    
        private void Awake()
        {
            contWeps = this;
        }

        public void AddWeapon(int weaponIndex) 
        {
            if (weaponIndex < allWeapons.Count)
            {
                equippedWeapons.Add(allWeapons[weaponIndex]);
                allWeapons[weaponIndex].gameObject.SetActive(true);
                allWeapons.RemoveAt(weaponIndex);
            }
        }

        public void AddWeapon(Weapon newWeapon)
        {
            newWeapon.gameObject.SetActive(true);
            equippedWeapons.Add(newWeapon);
            allWeapons.Remove(newWeapon);
        }
    }
}
