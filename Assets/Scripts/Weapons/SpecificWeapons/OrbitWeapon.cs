using System.Collections.Generic;
using Controllers;
using Damagers;
using GenUtilsAndTools;
using JetBrains.Annotations;
using UnityEngine;

namespace Weapons.SpecificWeapons
{
    public class OrbitWeapon : Weapon
    {
        [CanBeNull] public List<EnemyDamager> enemyDamagers;
        [CanBeNull] public List<GrowShrinkObj> projectileScales;

        protected float RotationSpeed;
        
        protected virtual void Start()
        {
            SetStats();
        }

        protected virtual void FixedUpdate()
        {
            if (RotationSpeed <= 0) RotationSpeed = stats.weaponLvls[stats.lvl].speed * Time.deltaTime;
        }

        protected virtual void Awake()
        {
            SetStats();
        }
        
        private void OnDisable()
        {
            juiceManager.StopFeedback(GenericJuiceManager.FeedbackType.Firing);
            juiceManager.StopFeedback(GenericJuiceManager.FeedbackType.Hit);
        }

        protected virtual void SetStats()
        {
            if (enemyDamagers == null) return;
            for (var i = 0; i < enemyDamagers.Count; i++)
            {
                enemyDamagers[i].damage = stats.weaponLvls[stats.lvl].damage;
                if (projectileScales != null)
                {
                    projectileScales[i].staySizeInterval = stats.weaponLvls[stats.lvl].duration * .6f;
                    projectileScales[i].maxSize = Vector3.one * stats.weaponLvls[stats.lvl].range;
                }
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
                }

            transform.localScale = stats.weaponLvls[stats.lvl].size;
            RotationSpeed = stats.weaponLvls[stats.lvl].speed * Time.deltaTime;
        }
    }
}