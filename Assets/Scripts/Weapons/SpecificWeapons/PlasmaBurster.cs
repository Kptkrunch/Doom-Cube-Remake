using System.Collections;
using Controllers.Pools;
using UnityEngine;
using Weapons.Projectiles;
using Random = UnityEngine.Random;

namespace Weapons.SpecificWeapons
{
    public class PlasmaBurster : PrefabBasedWeapon
    {
        private void Awake()
        {
            SetStats();
            canFire = true;
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
            RandomDirection();
            Debug.Log(direction);
            for (int i = 0; i < ammo; i++)
            {
                yield return new WaitForSeconds(fireInterval);
                var proj = ProjectilePoolManager.poolProj.projPools[stats.pid].GetPooledGameObject();
                var theProj = proj.GetComponent<Projectile>();
                proj.transform.position = transform.position;
                theProj.pd.stats.direction = direction;
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
        }

        private void RandomDirection()
        {
            direction = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)).normalized;
            var angle = Mathf.Atan2(direction.x, direction.y) * Mathf.Deg2Rad - 90;
            rotation = Quaternion.Euler(0f, 0f, angle).normalized;
        }
    }
}
