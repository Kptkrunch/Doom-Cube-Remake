using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitWeapon : Weapon
{
    public float rotationSpeed;
    public Transform projectileFrame;
    public List<EnemyDamager> projectileDamagers;
    public List<ChangeProjectileScale> projectileScales;
    public WeaponFrameTimer weaponFrameTimer;
    public GameObject ability1;
    public GameObject ability2;

    void Update()
    {
        transform.rotation = Quaternion.Euler(0f, 0f, projectileFrame.rotation.eulerAngles.z + (rotationSpeed * stats[0].projSpeed * Time.deltaTime));
        if (weaponLeveledUp == true)
        {
            weaponLeveledUp = false;
            UpdateWeapon();
        }
    }

    public void UpdateWeapon()
    {
        WeaponLevelUp();

        // update projectile damage, add more for more projectiles
        projectileDamagers[0].damage = stats[weaponLevel].damage;
        projectileDamagers[1].damage = stats[weaponLevel].damage;
        projectileDamagers[2].damage = stats[weaponLevel].damage;
        projectileDamagers[3].damage = stats[weaponLevel].damage;

        // increase rotational speed on projectiles
        rotationSpeed *= stats[weaponLevel].projSpeed;
        
        // increase radius
        transform.localScale = Vector3.one * stats[weaponLevel].range;
        
        // increase projectile size and growth speed
        projectileScales[0].maxSize *= stats[weaponLevel].size;
        projectileScales[1].maxSize *= stats[weaponLevel].size;
        projectileScales[2].maxSize *= stats[weaponLevel].size;
        projectileScales[3].maxSize *= stats[weaponLevel].size;
        
        // match growth interval to weapon frame interval
        // increase projectile size and growth speed
        projectileScales[0].staySizeInterval = weaponFrameTimer.activeInterval * .6f;
        projectileScales[1].staySizeInterval = weaponFrameTimer.activeInterval * .6f;
        projectileScales[2].staySizeInterval = weaponFrameTimer.activeInterval * .6f;
        projectileScales[3].staySizeInterval = weaponFrameTimer.activeInterval * .6f;
        
        // increase duration and reduce cooldown
        weaponFrameTimer.coolDownTimer *= stats[weaponLevel].cdr;
        weaponFrameTimer.activeInterval *= stats[weaponLevel].duration;
        
        // add weapon new abilities
        if (!ability1)
        {
            if (stats[weaponLevel].specialAbilities[0] == gameObject)
            {
                ability1 = stats[weaponLevel].specialAbilities[0];
            }
        }
        
        if (!ability2)
        {
            if (stats[weaponLevel].specialAbilities[1] == gameObject)
            {
                ability2 = stats[weaponLevel].specialAbilities[1];
            }
        }
    }
}
