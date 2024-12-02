using System.Collections;
using Controllers.Pools;
using MoreMountains.Feedbacks;
using UnityEngine;

namespace Weapons.SpecificWeapons
{
    public class MechanicalMeatSeperator : LobbedWeapon
    {
        private Vector3 _direction;
        private void Start()
        {
            SetStats();
            Player = WeaponSfxGroupController.Instance.sfxControllers[stats.wid].player;
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
                LaunchSawBlade();
                yield return new WaitForSeconds(FireInterval);
            }

            yield return new WaitForSeconds(Cooldown);
            CanFire = true;
        }

        private void SetStats()
        {
            CanFire = true;
            RateOfFire = stats.weaponLvls[stats.lvl].rateOfFire;
            Cooldown = stats.weaponLvls[stats.lvl].coolDown;
            Ammo = stats.weaponLvls[stats.lvl].ammo;
        }

        private void LaunchSawBlade()
        {
            var flash = MuzzleFlashPools.Instance.flashPools[stats.pid].GetPooledGameObject();
            flash.transform.position = transform.position;
            flash.SetActive(true);
            var saw = ProjectilePoolManager.poolProj.projPools[stats.pid].GetPooledGameObject();
            saw.transform.position = transform.position;
            saw.SetActive(true);
            Player.FeedbacksList[0].Play(transform.position);
        }
    }
}
