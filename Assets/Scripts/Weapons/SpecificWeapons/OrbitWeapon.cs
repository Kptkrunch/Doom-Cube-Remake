using System;
using System.Collections.Generic;
using Damagers;
using GenUtilsAndTools;
using UnityEngine;

namespace Weapons.SpecificWeapons
{
    public class OrbitWeapon : Weapon
    {
        // public Transform projectileFrame;
        public List<EnemyDamager> enemyDamagers;
        public List<GrowShrinkObj> projectileScales;
        public ProjFrameWithTimer projFrameWithTimer;
        
        private float _rotationSpeed;

        private void Start()
        {
            SetStats();
        }

        private void Update()
        {
            transform.rotation = Quaternion.Euler(0f, 0f, projFrameWithTimer.transform.rotation.eulerAngles.z + _rotationSpeed * stats[0].projSpeed * Time.deltaTime);
        }

        private void SetStats()
        {
            _rotationSpeed = stats[weaponLevel].projSpeed;
            for (int i = 0; i < enemyDamagers.Count; i++)
            {
                enemyDamagers[i].damage = stats[weaponLevel].damage;
                projectileScales[i].staySizeInterval = stats[weaponLevel].duration * .6f;
                projectileScales[i].maxSize = Vector3.one;
                projFrameWithTimer.activeInterval = stats[weaponLevel].duration;
                projFrameWithTimer.coolDownTimer = stats[weaponLevel].cdr;
            }
        }

        public override void UpdateWeapon()
        {
            WeaponLevelUp();

            // update projectile damage, add more for more projectiles
            // increase projectile size and growth speed
            // match growth interval to weapon frame interval
            // increase projectile size and growth speed
            for (int i = 0; i < enemyDamagers.Count; i++)
            {
                enemyDamagers[i].damage *= stats[weaponLevel].damage;
                projectileScales[i].staySizeInterval *= stats[weaponLevel].duration * .6f;
                projFrameWithTimer.activeInterval *= stats[weaponLevel].duration;
                projFrameWithTimer.coolDownTimer *= stats[weaponLevel].cdr;
            }
            // increase radius
            transform.localScale *= stats[weaponLevel].range;
            // increase duration and reduce cooldown
            projFrameWithTimer.coolDownTimer *= stats[weaponLevel].cdr;
            projFrameWithTimer.activeInterval *= stats[weaponLevel].duration;
            // increase rotational speed on projectiles
            _rotationSpeed *= stats[weaponLevel].projSpeed;

        }
    }
}
