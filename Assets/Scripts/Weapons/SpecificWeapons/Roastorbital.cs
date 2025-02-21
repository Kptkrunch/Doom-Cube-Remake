using JetBrains.Annotations;
using MoreMountains.Tools;
using UnityEngine;
using Weapons.WeaponModifiers;

namespace Weapons.SpecificWeapons
{
    public class Roastorbital : OrbitWeapon
    {
        public static Roastorbital Instance;
        public ProjFrameWithTimer projFrameWithTimer;
        public MMSimpleObjectPooler phaseParticlePool, hitParticlePool;

        protected override void Awake()
        {
            Instance = this;
            SetStats();

            if (projFrameWithTimer)
                transform.rotation = Quaternion.Euler(0f, 0f,
                    projFrameWithTimer.transform.rotation.eulerAngles.z +
                    RotationSpeed * stats.weaponLvls[stats.lvl].speed);
        }

        protected override void Start()
        {
            if (projFrameWithTimer != null) projFrameWithTimer.pid = stats.pid;
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
        }

        protected override void SetStats()
        {
            if (projFrameWithTimer)
                projFrameWithTimer.UpdateIntervals(stats.weaponLvls[stats.lvl].duration,
                    stats.weaponLvls[stats.lvl].coolDown);

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
                projFrameWithTimer.duration = stats.weaponLvls[stats.lvl].duration;
                projFrameWithTimer.coolDown = stats.weaponLvls[stats.lvl].coolDown;
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
                    projFrameWithTimer.duration = stats.weaponLvls[stats.lvl].duration;
                    projFrameWithTimer.coolDown = stats.weaponLvls[stats.lvl].coolDown;
                }

            transform.localScale = stats.weaponLvls[stats.lvl].size;
            if (projFrameWithTimer != null)
            {
                projFrameWithTimer.coolDown = stats.weaponLvls[stats.lvl].coolDown;
                projFrameWithTimer.duration = stats.weaponLvls[stats.lvl].duration;
            }

            RotationSpeed = stats.weaponLvls[stats.lvl].speed * Time.deltaTime;
        }
        
    }
}