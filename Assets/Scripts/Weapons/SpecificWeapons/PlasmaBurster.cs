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
            CanFire = true;
        }

        private void FixedUpdate()
        {
            if (CanFire) StartCoroutine(AttackLoop());
        }

        private IEnumerator AttackLoop()
        {
            CanFire = false;
            RandomDirection();
            Debug.Log(Direction);
            for (var i = 0; i < Ammo; i++)
            {
                yield return new WaitForSeconds(FireInterval);
                var proj = ProjectilePoolManager.poolProj.projPools[stats.pid].GetPooledGameObject();
                var theProj = proj.GetComponent<Projectile>();
                proj.transform.position = transform.position;
                theProj.pd.stats.direction = Direction;
                proj.SetActive(true);
                proj.transform.rotation = Rotation;
            }

            yield return new WaitForSeconds(ReloadInterval);
            CanFire = true;
        }

        private void SetStats()
        {
            FireInterval = stats.weaponLvls[stats.lvl].rateOfFire;
            ReloadInterval = stats.weaponLvls[stats.lvl].coolDown;
            Ammo = stats.weaponLvls[stats.lvl].ammo;
        }

        private void RandomDirection()
        {
            Direction = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)).normalized;
            var angle = Mathf.Atan2(Direction.x, Direction.y) * Mathf.Deg2Rad - 90;
            Rotation = Quaternion.Euler(0f, 0f, angle).normalized;
        }
    }
}