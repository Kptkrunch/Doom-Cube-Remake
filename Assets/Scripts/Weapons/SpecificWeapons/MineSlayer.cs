using System;
using System.Collections;
using Controllers.Pools;
using UnityEngine;

namespace Weapons.SpecificWeapons
{
    public class MineSlayer : PrefabBasedWeapon
    {
        private void Start()
        {
            SetStats();
        }

        private void FixedUpdate()
        {
            if (CanFire) StartCoroutine(AttackLoop());
        }

        private IEnumerator AttackLoop()
        {
            CanFire = false;
            for (var i = 0; i < Ammo; i++)
            {
                yield return new WaitForSeconds(stats.weaponLvls[stats.lvl].rateOfFire);
                Fire();
            }

            yield return new WaitForSeconds(stats.weaponLvls[stats.lvl].coolDown);
            CanFire = true;
        }

        protected override void Fire()
        {
            var mine = ProjectilePoolManager.poolProj.projPools[stats.pid].GetPooledGameObject();
            mine.transform.position = transform.position;
            mine.SetActive(true);
        }

        private void SetStats()
        {
            FireInterval = stats.weaponLvls[stats.lvl].rateOfFire;
            ReloadInterval = stats.weaponLvls[stats.lvl].coolDown;
            Ammo = stats.weaponLvls[stats.lvl].ammo;
        }
    }
}