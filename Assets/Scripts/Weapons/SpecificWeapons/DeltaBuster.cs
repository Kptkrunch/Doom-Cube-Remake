using System.Collections;
using System.Collections.Generic;
using Controllers.Pools;
using UnityEngine;
using Weapons.Projectiles;

namespace Weapons.SpecificWeapons
{
    public class DeltaBuster : PrefabBasedWeapon
    {
        public List<Transform> firePoints;
        public GameObject projectileFrame;

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
            direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            projectileFrame.transform.rotation = Quaternion.Euler(0f, 0f, angle);
            
            var coolDown = stats.weaponLvls[stats.lvl].coolDown;
            for (int j = 0; j < firePoints.Count; j++)
            {
                var proj = ProjectilePoolManager.poolProj.projPools[stats.pid].GetPooledGameObject();
                var projClass = proj.GetComponentInChildren<Projectile>();
                projClass.pd.stats.direction = direction;
                proj.transform.position = firePoints[j].position;
                proj.transform.rotation = Quaternion.Euler(0f, 0f, angle);
                proj.gameObject.SetActive(true);
            }
            yield return new WaitForSeconds(coolDown);
            canFire = true;
        }

        private void SetStats()
        {
            canFire = true;
            fireInterval = stats.weaponLvls[stats.lvl].rateOfFire;
            ammo = stats.weaponLvls[stats.lvl].ammo;
            
            
        }

        public override void UpdateWeapon()
        {
            var enemyDamager = ProjectilePoolManager.poolProj.projPools[stats.pid].GetComponent<Projectile>().enemyDamager;
            if (enemyDamager != null)
                enemyDamager.damage =
                    stats.weaponLvls[stats.lvl].damage;
            ProjectilePoolManager.poolProj.projPools[stats.pid].GetComponent<Projectile>().pd.stats.lifeTime = stats.weaponLvls[stats.lvl].speed;
            fireInterval = stats.weaponLvls[stats.lvl].rateOfFire;
        }
    }
}