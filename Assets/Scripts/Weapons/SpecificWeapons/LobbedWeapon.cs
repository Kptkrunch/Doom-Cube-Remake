using System;
using GenUtilsAndTools;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Weapons.SpecificWeapons
{
    public class LobbedWeapon : Weapon
    {
        public EnemyDamager enemyDamager;
        public EExplosionDamager eExplosionDamager;
        
        [SerializeField] private BombOrNade projectile;
        private float _attackInterval, _attackTimer, _direction, _lobHeight, _lobDistance;

        private void Start()
        {
            SetStats();
        }

        private void Update()
        {
            _attackTimer -= Time.deltaTime;
            if (_attackTimer <= 0)
            {
                _attackTimer = _attackInterval;
                for (var i = 0; i < stats[weaponLevel].numOfProj; i++)
                {
                    Transform transform1;
                    if (eExplosionDamager)
                    {
                        Instantiate(eExplosionDamager, (transform1 = eExplosionDamager.transform).position, transform1.rotation).gameObject.SetActive(true);
                    }
                    else
                    {
                        Instantiate(enemyDamager, (transform1 = enemyDamager.transform).position, transform1.rotation).gameObject.SetActive(true);
                    }
                }
            }
        }

        private void SetStats()
        {
            _attackInterval = stats[weaponLevel].rateOfFire;
            _attackTimer = _attackInterval;
            enemyDamager.damage = stats[weaponLevel].damage;
            projectile.lobHeight = stats[weaponLevel].range + 1f;
            projectile.lobDistance = stats[weaponLevel].range;
            projectile.lifeTimer = stats[weaponLevel].duration;
            projectile.rotationSpeed = stats[weaponLevel].projSpeed;
        }

        public override void UpdateWeapon()
        {
            _attackInterval = stats[weaponLevel].cdr;
            projectile.transform.localScale *= stats[weaponLevel].size;
            stats[weaponLevel].numOfProj = stats[weaponLevel].numOfProj;
            enemyDamager.damage = stats[weaponLevel].damage;
            projectile.lobHeight = stats[weaponLevel].range + 1f;
            projectile.lobDistance = stats[weaponLevel].range;
            projectile.lifeTimer = stats[weaponLevel].duration;
            projectile.rotationSpeed = stats[weaponLevel].projSpeed;
        }
    }
}
