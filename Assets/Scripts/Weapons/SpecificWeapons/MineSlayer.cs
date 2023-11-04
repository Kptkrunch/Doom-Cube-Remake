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
            if (canFire)
            {
                StartCoroutine(AttackLoop());
            }
        }

        IEnumerator AttackLoop()
        {
            canFire = false;
            for (var i = 0; i < ammo; i++)
            {
                yield return new WaitForSeconds(stats.weaponLvls[stats.lvl].rateOfFire);
                Fire();
            }

            yield return new WaitForSeconds(stats.weaponLvls[stats.lvl].coolDown);
            canFire = true;
        }

        protected override void Fire()
        {
            var mine = ProjectilePoolManager.poolProj.projPools[stats.pid].GetPooledGameObject();
            mine.transform.position = transform.position;
            mine.SetActive(true);
        }

        private void SetStats()
        {
            fireInterval = stats.weaponLvls[stats.lvl].rateOfFire;
            reloadInterval = stats.weaponLvls[stats.lvl].coolDown;
            ammo = stats.weaponLvls[stats.lvl].ammo;
        }   
    }
}
