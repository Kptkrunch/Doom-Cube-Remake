using System.Collections;
using System.Collections.Generic;
using Controllers;
using Controllers.Pools;
using UnityEngine;
using UnityEngine.Serialization;
using Weapons.Projectiles;
using Random = UnityEngine.Random;

namespace Weapons.SpecificWeapons
{
    public class DirectionalWeapon : PrefabBasedWeapon
    {
        [SerializeField] protected Vector2 dir1;
        [SerializeField] protected Vector2 dir2;
        [SerializeField] protected bool doesAlternate;

        private void FixedUpdate()
        {
            if (CanFire)
            {
                StartCoroutine(AttackLoop());
            }
        }

        IEnumerator AttackLoop()
        {
            CanFire = false;
            for (var i = 0; i < stats.weaponLvls[stats.lvl].ammo; i++)
            {


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