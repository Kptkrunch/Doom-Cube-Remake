using System.Collections.Generic;
using Damagers;
using GenUtilsAndTools;
using JetBrains.Annotations;
using UnityEngine;

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
                    _rotationSpeed * stats[weaponLevel].projSpeed * Time.deltaTime);
            
            if (naniteController)
            {
                transform.rotation = Quaternion.Euler(0f, 0f,
                    naniteController.gameObject.transform.rotation.eulerAngles.z +
                    _rotationSpeed * stats[weaponLevel].projSpeed * Time.deltaTime);
            }
        }

        private void SetStats()
        {
            _rotationSpeed = stats[weaponLevel].projSpeed;
            if (enemyDamagers == null) return;
            for (var i = 0; i < enemyDamagers.Count; i++)
            {
                enemyDamagers[i].damage = stats[weaponLevel].damage;
                if (projectileScales != null)
                {
                    projectileScales[i].staySizeInterval = stats[weaponLevel].duration * .6f;
                    projectileScales[i].maxSize = Vector3.one * stats[weaponLevel].size;
                }

                if (!projFrameWithTimer) continue;
                projFrameWithTimer.activeInterval = stats[weaponLevel].duration;
                projFrameWithTimer.coolDownTimer = stats[weaponLevel].cdr;
            }
        }

        public override void UpdateWeapon()
        {
            WeaponLevelUp();

            if (enemyDamagers != null)
                for (var i = 0; i < enemyDamagers.Count; i++)
                {
                    enemyDamagers[i].damage = stats[weaponLevel].damage;
                    if (projectileScales != null)
                        projectileScales[i].staySizeInterval = stats[weaponLevel].duration * .6f;
                    if (projFrameWithTimer == null) continue;
                    projFrameWithTimer.activeInterval = stats[weaponLevel].duration;
                    projFrameWithTimer.coolDownTimer = stats[weaponLevel].cdr;
                }

            // increase radius
            transform.localScale *= stats[weaponLevel].size;
            // increase duration and reduce cooldown
            if (projFrameWithTimer != null)
            {
                projFrameWithTimer.coolDownTimer = stats[weaponLevel].cdr;
                projFrameWithTimer.activeInterval = stats[weaponLevel].duration;
            }

            // increase rotational speed on projectiles
            _rotationSpeed = stats[weaponLevel].projSpeed;

        }
    }
}
