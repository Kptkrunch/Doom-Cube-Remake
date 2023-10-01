using System;
using Controllers;
using GenUtilsAndTools;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Weapons.SpecificWeapons
{
    public class ProjectileWeapon : Weapon
    {
        public Projectile projectile;
        private float _weaponRange;
        private float _fireTimer;
        public LayerMask enemyLayer;

        private void Start()
        {
            SetStats();
        }

        private void SetStats()
        {
            projectile.enemyDamager.damage = stats[weaponLevel].damage;
            projectile.moveSpeed = stats[weaponLevel].projSpeed;
            _fireTimer = stats[weaponLevel].rateOfFire;
            _weaponRange = stats[weaponLevel].range;
        }

        private void Update()
        {
            _fireTimer -= Time.deltaTime;
            if (_fireTimer <= 0)
            {
                _fireTimer = stats[weaponLevel].rateOfFire;
                Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, _weaponRange, enemyLayer);
                if (enemies.Length > 0)
                {
                    for (var i = 0; i < stats[weaponLevel].numOfProj; i++)
                    {
                        var targetPosition = enemies[Random.Range(0, enemies.Length)].transform.position;

                        var position = transform.position;
                        var targetDirection = targetPosition - position;
                        var angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
                        angle -= 90;
                        projectile.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

                        Instantiate(projectile, position, projectile.transform.rotation).gameObject.SetActive(true);
                    }
                } 
            }
        }
    }
}
