using System;
using Controllers;
using GenUtilsAndTools;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Weapons.SpecificWeapons
{
    public class RadarWeapon : Weapon
    {
        public Projectile projectile;
        private float _weaponRange, _fireTimer, _fireInterval;
        public LayerMask enemyLayer;

        private void Start()
        {
            SetStats();
        }

        private void SetStats()
        {
            projectile.enemyDamager.damage = stats[weaponLevel].damage;
            projectile.moveSpeed = stats[weaponLevel].projSpeed;
            _fireInterval = stats[weaponLevel].rateOfFire;
            _fireTimer = _fireInterval;
            _weaponRange = stats[weaponLevel].range;
            projectile.numberOfPenetrates = stats[weaponLevel].duration;
        }

        private void Update()
        {
            _fireTimer -= Time.deltaTime;
            if (_fireTimer <= 0)
            {
                _fireTimer = _fireInterval;
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
        
        public override void UpdateWeapon()
        {
            WeaponLevelUp();

            // update projectile damage and speed
            projectile.enemyDamager.damage = stats[weaponLevel].damage;
            projectile.moveSpeed *= stats[weaponLevel].projSpeed;
            projectile.numberOfPenetrates = stats[weaponLevel].duration;
            // increase detection range
            _weaponRange *= stats[weaponLevel].range;        
        
            // reduce time between volleys
            _fireInterval *= stats[weaponLevel].cdr;
        }
    }
}