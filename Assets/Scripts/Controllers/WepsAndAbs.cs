using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WepsAndAbs : MonoBehaviour
{
    public static WepsAndAbs wepsAndAbs;
    public int maxWeapons;
    public List<Weapon> equippedWeapons, upgradableWeapons, allWeapons;
    private void Awake()
    {
        wepsAndAbs = this;
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
