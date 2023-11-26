using System.Collections;
using Controllers.Pools;
using UnityEngine;

namespace Weapons.SpecificWeapons
{
    public class LobbedWeapon : PrefabBasedWeapon
    {
        protected float RateOfFire, Cooldown;
        private void Start()
        {
            SetStats();
        }

        private void FixedUpdate()
        {
            if (CanFire)
            {
                StartCoroutine(AttackLoop());
            }
        }

        private void SetStats()
        {
            CanFire = true;
            RateOfFire = stats.weaponLvls[stats.lvl].rateOfFire;
            Cooldown = stats.weaponLvls[stats.lvl].coolDown;
            Ammo = stats.weaponLvls[stats.lvl].ammo;
        }

        IEnumerator AttackLoop()
        {
            CanFire = false;

            for (var i = 0; i < Ammo; i++)
            {
                LobProjectile();
                yield return new WaitForSeconds(RateOfFire);
            }

            yield return new WaitForSeconds(Cooldown);
            CanFire = true;
        }

        public override void UpdateWeapon()
        {
            RateOfFire = stats.weaponLvls[stats.lvl].rateOfFire;
            Cooldown = stats.weaponLvls[stats.lvl].coolDown;
            Ammo = stats.weaponLvls[stats.lvl].ammo;
        }
        
        private void LobProjectile()
        {
            var saw = ProjectilePoolManager.poolProj.projPools[stats.pid].GetPooledGameObject();
            saw.transform.position = transform.position;
            saw.SetActive(true);
        }
    }
}
