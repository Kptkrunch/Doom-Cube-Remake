using System;
using System.Collections;
using Controllers.Pools;
using UnityEngine;
using UnityEngine.Serialization;
using Weapons.Projectiles;
using Random = UnityEngine.Random;

namespace Weapons.SpecificWeapons
{
    public class RapidFireWeapon : Weapon
    {
        private float _fireInterval, _reloadInterval;
        [FormerlySerializedAs("_canFire")] public bool canFire;
        private Vector2 _direction;
        private Quaternion _rotation;
        private float _numOfProjectiles;

        void Start()
        {
            SetStats();
            canFire = true;
        }
        private void FixedUpdate()
        {
            if (canFire)
            {
                StartCoroutine(RapidFire());
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Enemy") && !collision.CompareTag("Player"))
            {
                _direction = (collision.transform.position - transform.position).normalized;
            }
        }

        IEnumerator RapidFire()
        {
            canFire = false;
            var dir = _direction;
            for (int i = 0; i < _numOfProjectiles; i++)
            {
                yield return new WaitForSeconds(_fireInterval);
                var proj = ProjectilePoolManager.poolProj.projPools[1].GetPooledGameObject();
                var theProj = proj.GetComponent<Projectile>();
                proj.transform.position = transform.position;
                theProj.direction = dir;
                proj.SetActive(true);
                proj.transform.rotation = _rotation;
            }
            yield return new WaitForSeconds(_reloadInterval);
            canFire = true;
        }
        
        private void SetStats()
        {
            _fireInterval = stats.weaponLvls[stats.lvl].rateOfFire;
            _reloadInterval = stats.weaponLvls[stats.lvl].coolDown;
            _numOfProjectiles = stats.weaponLvls[stats.lvl].ammo;
            var attackRadius = GetComponent<CircleCollider2D>();
            attackRadius.radius = stats.weaponLvls[stats.lvl].range;
        }

        private void RandomDirection()
        {
            _direction = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)).normalized;
            var angle = Mathf.Atan2(_direction.x, _direction.y) * Mathf.Deg2Rad - 90;
            var rotation = Quaternion.Euler(0f, 0f, angle).normalized;
            _rotation = rotation;
        }
    }
}
