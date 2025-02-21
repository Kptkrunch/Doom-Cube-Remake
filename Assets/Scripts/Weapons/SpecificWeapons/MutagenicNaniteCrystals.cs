using System;
using MoreMountains.Tools;
using UnityEngine;
using Weapons.WeaponModifiers;

namespace Weapons.SpecificWeapons
{
    public class MutagenicNaniteCrystals : OrbitWeapon
    {
        public static MutagenicNaniteCrystals Instance;
        public NaniteController naniteController;
        public MMSimpleObjectPooler phaseParticlePool, hitParticlePool;
        
        
        protected override void Awake()
        {
            base.Awake();
            if (naniteController)
                naniteController.nanitePhasers[0].UpdateIntervals(stats.weaponLvls[stats.lvl].duration,
                    stats.weaponLvls[stats.lvl].duration);
            if (naniteController)
                naniteController.nanitePhasers[1].UpdateIntervals(stats.weaponLvls[stats.lvl].duration,
                    stats.weaponLvls[stats.lvl].duration);
            Instance = this;
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            if (naniteController)
                transform.rotation = Quaternion.Euler(0f, 0f,
                    naniteController.gameObject.transform.rotation.eulerAngles.z +
                    RotationSpeed * stats.weaponLvls[stats.lvl].speed);
        }
    }
}