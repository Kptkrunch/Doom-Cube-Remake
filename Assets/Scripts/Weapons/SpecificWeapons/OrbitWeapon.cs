using System.Collections.Generic;
using Damagers;
using GenUtilsAndTools;
using JetBrains.Annotations;
using UnityEngine;
using Weapons.WeaponModifiers;

namespace Weapons.SpecificWeapons
{
    public class OrbitWeapon : Weapon
    {
        // public Transform projectileFrame;
        [CanBeNull] public List<EnemyDamager> enemyDamagers;
        [CanBeNull] public List<GrowShrinkObj> projectileScales;
        [CanBeNull] public ProjFrameWithTimer projFrameWithTimer;
        [CanBeNull] public NaniteController naniteController;
        
        private float _rotationSpeed;

        private void Start()
        {
            SetStats();
        }

        private void Update()
        {
            if (projFrameWithTimer)
                transform.rotation = Quaternion.Euler(0f, 0f,
                    projFrameWithTimer.transform.rotation.eulerAngles.z +
                    _rotationSpeed * stats.weaponLvls[stats.lvl].speed * Time.deltaTime);
            
            if (naniteController)
            {
                transform.rotation = Quaternion.Euler(0f, 0f,
                    naniteController.gameObject.transform.rotation.eulerAngles.z +
                    _rotationSpeed * stats.weaponLvls[stats.lvl].speed * Time.deltaTime);
            }
        }

        private void SetStats()
        {
            _rotationSpeed = stats.weaponLvls[stats.lvl].speed;
            if (enemyDamagers == null) return;
            for (var i = 0; i < enemyDamagers.Count; i++)
            {
                enemyDamagers[i].damage = stats.weaponLvls[stats.lvl].damage;
                if (projectileScales != null)
                {
                    projectileScales[i].staySizeInterval = stats.weaponLvls[stats.lvl].duration * .6f;
                    projectileScales[i].maxSize = Vector3.one * stats.weaponLvls[stats.lvl].range;
                }

                if (!projFrameWithTimer) continue;
                projFrameWithTimer.activeInterval = stats.weaponLvls[stats.lvl].duration;
                projFrameWithTimer.coolDownTimer = stats.weaponLvls[stats.lvl].coolDown;
            }
        }

        public override void UpdateWeapon()
        {
            WeaponLevelUp();

            if (enemyDamagers != null)
                for (var i = 0; i < enemyDamagers.Count; i++)
                {
                    enemyDamagers[i].damage = stats.weaponLvls[stats.lvl].damage;
                    if (projectileScales != null)
                        projectileScales[i].staySizeInterval = stats.weaponLvls[stats.lvl].duration * .6f;
                    if (projFrameWithTimer == null) continue;
                    projFrameWithTimer.activeInterval = stats.weaponLvls[stats.lvl].duration;
                    projFrameWithTimer.coolDownTimer = stats.weaponLvls[stats.lvl].coolDown;
                }

            // increase radius
            transform.localScale *= stats.weaponLvls[stats.lvl].range;
            // increase duration and reduce cooldown
            if (projFrameWithTimer != null)
            {
                projFrameWithTimer.coolDownTimer = stats.weaponLvls[stats.lvl].coolDown;
                projFrameWithTimer.activeInterval = stats.weaponLvls[stats.lvl].duration;
            }

            // increase rotational speed on projectiles
            _rotationSpeed = stats.weaponLvls[stats.lvl].speed;

        }
    }
}
