using System.Collections.Generic;
using Controllers;
using Damagers;
using GenUtilsAndTools;
using JetBrains.Annotations;
using MoreMountains.Feedbacks;
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
            if (projFrameWithTimer != null) projFrameWithTimer.pid = stats.pid;
        }

        private void FixedUpdate()
        {
            if (_rotationSpeed <= 0) _rotationSpeed = stats.weaponLvls[stats.lvl].speed * Time.deltaTime;
            
            if (projFrameWithTimer)
                transform.rotation = Quaternion.Euler(0f, 0f,
                    projFrameWithTimer.transform.rotation.eulerAngles.z +
                    _rotationSpeed * stats.weaponLvls[stats.lvl].speed);

            if (naniteController)
                transform.rotation = Quaternion.Euler(0f, 0f,
                    naniteController.gameObject.transform.rotation.eulerAngles.z +
                    _rotationSpeed * stats.weaponLvls[stats.lvl].speed);
        }

        private void OnAwake()
        {
            SetStats();
        }
        
        private void OnDisable()
        {
            juiceManager.StopFeedback(GenericJuiceManager.FeedbackType.Firing);
        }

        private void SetStats()
        {
            if (naniteController) naniteController.nanitePhasers[0].UpdateIntervals(stats.weaponLvls[stats.lvl].duration, stats.weaponLvls[stats.lvl].duration);
            if (naniteController) naniteController.nanitePhasers[1].UpdateIntervals(stats.weaponLvls[stats.lvl].duration, stats.weaponLvls[stats.lvl].duration);

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

            transform.localScale = stats.weaponLvls[stats.lvl].size;
            if (projFrameWithTimer != null)
            {
                projFrameWithTimer.coolDownTimer = stats.weaponLvls[stats.lvl].coolDown;
                projFrameWithTimer.activeInterval = stats.weaponLvls[stats.lvl].duration;
            }

            _rotationSpeed = stats.weaponLvls[stats.lvl].speed * Time.deltaTime;
        }
    }
}