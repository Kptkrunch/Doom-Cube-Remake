using System.Collections;
using Controllers.Pools;
using MoreMountains.Feedbacks;
using UnityEngine;

namespace Weapons.SpecificWeapons
{
    public class LobbedWeapon : PrefabBasedWeapon
    {
        protected float RateOfFire, Cooldown;
        protected MMF_Player Player;

        private void Start()
        {
            SetStats();
            Player = WeaponSfxGroupController.Instance.sfxControllers[stats.wid].player;
        }

        private void FixedUpdate()
        {
            if (CanFire) StartCoroutine(AttackLoop());
        }


        private void SetStats()
        {
            CanFire = true;
            RateOfFire = stats.weaponLvls[stats.lvl].rateOfFire;
            Cooldown = stats.weaponLvls[stats.lvl].coolDown;
            Ammo = stats.weaponLvls[stats.lvl].ammo;
        }

        private IEnumerator AttackLoop()
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
            var flash = MuzzleFlashPools.Instance.flashPools[stats.pid].GetPooledGameObject();
            flash.transform.position = transform.position;
            flash.SetActive(true);
            Player.FeedbacksList[0].Play(transform.position);

            var proj = ProjectilePoolManager.poolProj.projPools[stats.pid].GetPooledGameObject();
            proj.transform.position = transform.position;
            proj.SetActive(true);
        }
    }
}