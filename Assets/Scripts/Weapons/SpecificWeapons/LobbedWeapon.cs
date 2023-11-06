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
                Debug.Log("in the can fire");
                StartCoroutine(AttackLoop());
            }
        }

        IEnumerator AttackLoop()
        {
            CanFire = false;
            Debug.Log("canfire false");

            for (var i = 0; i < Ammo; i++)
            {
                Debug.Log("in the loop");

                var proj = ProjectilePoolManager.poolProj.projPools[stats.pid].GetPooledGameObject();
                proj.transform.position = transform.position;
                proj.SetActive(true);

                yield return new WaitForSeconds(RateOfFire);
            }
            Debug.Log("out of the loop");

            yield return new WaitForSeconds(Cooldown);
            CanFire = true;
            Debug.Log("canfire true");

        }

        private void SetStats()
        {
            CanFire = true;
            RateOfFire = stats.weaponLvls[stats.lvl].rateOfFire;
            Cooldown = stats.weaponLvls[stats.lvl].coolDown;
        }

        public override void UpdateWeapon()
        {
            stats.weaponLvls[stats.lvl].ammo = stats.weaponLvls[stats.lvl].ammo;
        }
    }
}
