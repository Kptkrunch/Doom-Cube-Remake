using System.Collections;
using Controllers;
using Controllers.Pools;
using UnityEngine;
using Weapons.Projectiles;

namespace Weapons.SpecificWeapons
{
    public class DirectionalWeapon : PrefabBasedWeapon
    {
        [SerializeField] protected Vector2 dir1;
        [SerializeField] protected Vector2 dir2;
        [SerializeField] protected bool doesAlternate;
        protected float Cooldown;

        private void FixedUpdate()
        {
            if (CanFire) StartCoroutine(AttackLoop());
        }

        protected virtual IEnumerator AttackLoop()
        {
            CanFire = false;
            for (var i = 0; i < stats.weaponLvls[stats.lvl].ammo; i++)
            {
                juiceManager.TriggerFeedback(GenericJuiceManager.FeedbackType.Firing);
                var flash = MuzzleFlashPools.Instance.flashPools[stats.pid].GetPooledGameObject();
                flash.transform.position = transform.position;
                flash.SetActive(true);
                switch (doesAlternate)
                {
                    case true:
                    {
                        var proj = ProjectilePoolManager.poolProj.projPools[stats.pid].GetPooledGameObject();
                        proj.transform.position = transform.position;
                        
                        if (i % 2 == 0) proj.GetComponent<Projectile>().pd.stats.direction = dir1;
                        if (i % 2 != 0) proj.GetComponent<Projectile>().pd.stats.direction = dir2;
                        proj.SetActive(true);
                        break;
                    }
                    case false:
                    {
                        var proj = ProjectilePoolManager.poolProj.projPools[stats.pid].GetPooledGameObject();
                        proj.transform.position = transform.position;

                        proj.GetComponent<Projectile>().pd.stats.direction = dir1;

                        proj.SetActive(true);
                        break;
                    }
                }
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