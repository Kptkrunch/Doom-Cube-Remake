using Controllers.Pools;
using UnityEngine;

namespace Weapons.SpecificWeapons
{
    public class LobbedWeapon : Weapon
    {
        public int projectileIndex;
        private float _attackInterval, _attackTimer;
        private Vector3 _direction;
        private void Start()
        {
            SetStats();
        }

        private void FixedUpdate()
        {
            _attackTimer -= Time.deltaTime;
            if (_attackTimer <= 0) LaunchProjectile();
        }

        private void SetStats()
        {
            _attackInterval = stats.weaponLvls[stats.lvl].rateOfFire;
            _attackTimer = _attackInterval;
        }

        public override void UpdateWeapon()
        {
            _attackInterval = stats.weaponLvls[stats.lvl].rateOfFire;
            stats.weaponLvls[stats.lvl].ammo = stats.weaponLvls[stats.lvl].ammo;
        }

        private void LaunchProjectile()
        {
            _attackTimer = _attackInterval;
            for (var i = 0; i < stats.weaponLvls[stats.lvl].ammo; i++)
            {
                var bomb = ProjectilePoolManager.poolProj.projPools[projectileIndex].GetPooledGameObject();
                bomb.transform.position = transform.position;
                bomb.gameObject.SetActive(true);
            }
        }
    }
}
