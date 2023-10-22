using System.Collections.Generic;
using Controllers.Pools;
using UnityEngine;

namespace Weapons
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
                proj.GetComponentInChildren<Projectile>().MoveProjectile(-_direction, true);
            }
        }

        private void SetStats()
        {
            _moveSpeed = stats[weaponLevel].projSpeed;
            _fireInterval = stats[weaponLevel].rateOfFire;
            _fireTimer = _fireInterval;
        }

        public override void UpdateWeapon()
        {
            ProjectilePoolManager.poolProj.projPools[1].GetComponent<Projectile>().enemyDamager.damage = stats[weaponLevel].damage;
            ProjectilePoolManager.poolProj.projPools[1].GetComponent<Projectile>().lifeTimer = stats[weaponLevel].projSpeed;
            _moveSpeed = stats[weaponLevel].projSpeed;
            _fireInterval = stats[weaponLevel].rateOfFire;
            _fireTimer = _fireInterval;
        }
    }
}