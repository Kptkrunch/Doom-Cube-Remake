using System.Collections.Generic;
using EffectsTools;
using UnityEngine;

namespace Weapons.SpecificWeapons
{
    public class OrbitWeapon : Weapon
    {
        public float rotationSpeed;
        // public Transform projectileFrame;
        public List<EnemyDamager> projectileDamagers;
        public List<ChangeProjectileScale> projectileScales;
        public ProjFrameWithTimer projFrameWithTimer;

        private void Update()
        {
            transform.rotation = Quaternion.Euler(0f, 0f, projFrameWithTimer.transform.rotation.eulerAngles.z + (rotationSpeed * stats[0].projSpeed * Time.deltaTime));
        }

        public override void UpdateWeapon()
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
            projectileScales[0].staySizeInterval = projFrameWithTimer.activeInterval * .6f;
            projectileScales[1].staySizeInterval = projFrameWithTimer.activeInterval * .6f;
            projectileScales[2].staySizeInterval = projFrameWithTimer.activeInterval * .6f;
            projectileScales[3].staySizeInterval = projFrameWithTimer.activeInterval * .6f;
        
            // increase duration and reduce cooldown
            projFrameWithTimer.coolDownTimer *= stats[weaponLevel].cdr;
            projFrameWithTimer.activeInterval *= stats[weaponLevel].duration;
        }
    }
}
