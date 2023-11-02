using System.Collections.Generic;
using Controllers.Pools;
using UnityEngine;
using Weapons.Projectiles;

namespace Weapons.SpecificWeapons
{
    public class DeltaBuster : Weapon
    {
        public List<Transform> firePoints;
        public GameObject projectileFrame;

        private Vector2 _direction;
        private float _fireTimer, _fireInterval, _moveSpeed;

        private void Start()
        {
            SetStats();
        }

        private void FixedUpdate()
        {
            _fireTimer -= Time.deltaTime;
            if (_fireTimer <= 0)
            {
                _fireTimer = _fireInterval;
                FireBuster();
            }
        }

        private void FireBuster()
        {
            _direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
            float angle = Mathf.Atan2(-_direction.y, -_direction.x) * Mathf.Rad2Deg;
            projectileFrame.transform.rotation = Quaternion.Euler(0f, 0f, angle);
            
            for (int i = 0; i < firePoints.Count; i++)
            {
                var proj = ProjectilePoolManager.poolProj.projPools[1].GetPooledGameObject();
                proj.transform.position = firePoints[i].position;
                proj.gameObject.SetActive(true);
                proj.transform.rotation = Quaternion.Euler(0f, 0f, angle);
                proj.GetComponentInChildren<Projectile>().MoveProjectile();
            }
        }

        private void SetStats()
        {
            _moveSpeed = stats.weaponLvls[stats.lvl].speed;
            _fireInterval = stats.weaponLvls[stats.lvl].rateOfFire;
            _fireTimer = _fireInterval;
        }

        public override void UpdateWeapon()
        {
            var enemyDamager = ProjectilePoolManager.poolProj.projPools[1].GetComponent<Projectile>().enemyDamager;
            if (enemyDamager != null)
                enemyDamager.damage =
                    stats.weaponLvls[stats.lvl].damage;
            ProjectilePoolManager.poolProj.projPools[1].GetComponent<Projectile>().lifeTimer = stats.weaponLvls[stats.lvl].speed;
            _moveSpeed = stats.weaponLvls[stats.lvl].speed;
            _fireInterval = stats.weaponLvls[stats.lvl].rateOfFire;
            _fireTimer = _fireInterval;
        }
    }
}