using System.Collections;
using Controllers.Pools;
using UnityEngine;
using Weapons.Projectiles;
using Random = UnityEngine.Random;

namespace Weapons.SpecificWeapons
{
    public class RapidFireWeapon : PrefabBasedWeapon
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Enemy") && !collision.CompareTag("Player"))
            {
                direction = (collision.transform.position - transform.position).normalized;
            }
        }

        private void FixedUpdate()
        {
            if (canFire)
            {
                StartCoroutine(RapidFire());
            }
        }

        IEnumerator RapidFire()
        {
            canFire = false;
            var dir = direction;
            for (int i = 0; i < ammo; i++)
            {
                yield return new WaitForSeconds(fireInterval);
                var proj = ProjectilePoolManager.poolProj.projPools[1].GetPooledGameObject();
                var theProj = proj.GetComponent<Projectile>();
                proj.transform.position = transform.position;
                theProj.pd.stats.direction = dir;
                proj.SetActive(true);
                proj.transform.rotation = rotation;
            }
            yield return new WaitForSeconds(reloadInterval);
            canFire = true;
        }
        
        private void SetStats()
        {
            fireInterval = stats.weaponLvls[stats.lvl].rateOfFire;
            reloadInterval = stats.weaponLvls[stats.lvl].coolDown;
            ammo = stats.weaponLvls[stats.lvl].ammo;
            var attackRadius = GetComponent<CircleCollider2D>();
            attackRadius.radius = stats.weaponLvls[stats.lvl].range;
        }

        private void RandomDirection()
        {
            direction = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)).normalized;
            var angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Deg2Rad - 90;
            rotation = Quaternion.Euler(0f, 0f, angle).normalized;
        }
    }
}
