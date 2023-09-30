using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        }
    }
}

[System.Serializable]
public class WeaponStats
{
    public float projSpeed, damage, range, rateOfFire, numOfProj, duration, size, cdr;
    public string name, description, upgradeText;
    public List<GameObject> specialAbilities;
}
