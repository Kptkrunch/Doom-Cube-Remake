using System.Collections;
using Controllers.Pools;
using MoreMountains.Feedbacks;
using UnityEngine;
using Weapons.Projectiles;
using Weapons.SOS;

namespace Weapons.SpecificWeapons
{
    public class DirectionalWeapon : PrefabBasedWeapon
    {
        [SerializeField] protected Vector2 dir1;
        [SerializeField] protected Vector2 dir2;
        [SerializeField] protected bool doesAlternate;
        
        protected MMF_Player Player;
        protected float Cooldown;

        private void Start()
        {
            Player = WeaponSfxGroupController.Instance.sfxControllers[stats.wid].player;
        }

        private void FixedUpdate()
        {
            if (CanFire) StartCoroutine(AttackLoop());
        }

        protected virtual IEnumerator AttackLoop()
        {
            CanFire = false;
            for (var i = 0; i < stats.weaponLvls[stats.lvl].ammo; i++)
            {
                var flash = MuzzleFlashPools.Instance.flashPools[stats.pid].GetPooledGameObject();
                flash.transform.position = transform.position;
                flash.SetActive(true);
                if (doesAlternate)
                {
                    var proj = ProjectilePoolManager.poolProj.projPools[stats.pid].GetPooledGameObject();
                    proj.transform.position = transform.position;
                    if (i % 2 == 0) proj.GetComponent<Projectile>().pd.stats.direction = dir1;
                    if (i % 2 != 0) proj.GetComponent<Projectile>().pd.stats.direction = dir2;

                    proj.SetActive(true);
                }

                if (!doesAlternate)
                {
                    var proj = ProjectilePoolManager.poolProj.projPools[stats.pid].GetPooledGameObject();

                    proj.transform.position = transform.position;

                    proj.GetComponent<Projectile>().pd.stats.direction = dir1;

                    proj.SetActive(true);
                }

                if (!doesAlternate)
                {
                    var proj = ProjectilePoolManager.poolProj.projPools[stats.pid].GetPooledGameObject();

                    proj.transform.position = transform.position;

                    proj.GetComponent<Projectile>().pd.stats.direction = dir2;

                    proj.SetActive(true);
                }

                Player.FeedbacksList[0].Play(transform.position);
                yield return new WaitForSeconds(stats.weaponLvls[stats.lvl].rateOfFire);
            }

            yield return new WaitForSeconds(stats.weaponLvls[stats.lvl].coolDown);
            CanFire = true;
        }


        public override void UpdateWeapon()
        {
            WeaponLevelUp();
        }
    }
}