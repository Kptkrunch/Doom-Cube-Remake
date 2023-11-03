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
    
        protected override void Fire()
        {
            var mine = ProjectilePoolManager.poolProj.projPools[4].GetPooledGameObject();
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
