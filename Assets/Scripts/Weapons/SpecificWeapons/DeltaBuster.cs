using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Controllers;
using Controllers.Pools;
using MoreMountains.Feedbacks;
using UnityEngine;
using Weapons.Projectiles;

namespace Weapons.SpecificWeapons
{
    public class DeltaBuster : DirectionalWeapon
    {
        public List<Transform> firePoints;
        public GameObject projectileFrame;

        private void Start()
        {
            SetStats();
        }

        private void FixedUpdate()
        {
            if (CanFire) StartCoroutine(AttackLoop());
        }

        protected override IEnumerator AttackLoop()
        {
            CanFire = false;
            for (var i = 0; i < stats.weaponLvls[stats.lvl].ammo; i++)
            {
                if (doesAlternate)
                    for (var j = 0; j < firePoints.Count; j++)
                    {
                        Fire(i);
                    }

                if (!doesAlternate)
                    for (var j = 0; j < firePoints.Count(); j++)
                    {
                        var proj = ProjectilePoolManager.poolProj.projPools[stats.pid].GetPooledGameObject();

                        proj.transform.position = transform.position;

                        proj.GetComponent<Projectile>().pd.stats.direction = dir1;

                        proj.SetActive(true);
                    }

                if (!doesAlternate)
                    for (var j = 0; j < firePoints.Count(); j++)
                    {
                        var proj = ProjectilePoolManager.poolProj.projPools[stats.pid].GetPooledGameObject();

                        proj.transform.position = transform.position;

                        proj.GetComponent<Projectile>().pd.stats.direction = dir2;

                        proj.SetActive(true);
                    }

                yield return new WaitForSeconds(stats.weaponLvls[stats.lvl].rateOfFire);
            }

            yield return new WaitForSeconds(stats.weaponLvls[stats.lvl].coolDown);
            CanFire = true;
        }

        private void Fire(int index)
        {
            var flash = MuzzleFlashPools.Instance.flashPools[stats.pid].GetPooledGameObject();
            flash.transform.position = transform.position;
            flash.SetActive(true);
            var proj = ProjectilePoolManager.poolProj.projPools[stats.pid].GetPooledGameObject();
            proj.transform.position = transform.position;
            if (index % 2 == 0) proj.GetComponent<Projectile>().pd.stats.direction = dir1;
            if (index % 2 != 0) proj.GetComponent<Projectile>().pd.stats.direction = dir2;

            proj.SetActive(true);
            juiceManager.TriggerFeedback(GenericJuiceManager.FeedbackType.Firing);
        }

        private void SetStats()
        {
            CanFire = true;
            FireInterval = stats.weaponLvls[stats.lvl].rateOfFire;
            Cooldown = stats.weaponLvls[stats.lvl].coolDown;
            Ammo = stats.weaponLvls[stats.lvl].ammo;
        }

        public override void UpdateWeapon()
        {
            var enemyDamager = ProjectilePoolManager.poolProj.projPools[stats.pid].GetComponent<Projectile>()
                .enemyDamager;
            if (enemyDamager != null)
                enemyDamager.damage =
                    stats.weaponLvls[stats.lvl].damage;
            ProjectilePoolManager.poolProj.projPools[stats.pid].GetComponent<Projectile>().pd.stats.lifeTime =
                stats.weaponLvls[stats.lvl].speed;
            FireInterval = stats.weaponLvls[stats.lvl].rateOfFire;
        }
    }
}