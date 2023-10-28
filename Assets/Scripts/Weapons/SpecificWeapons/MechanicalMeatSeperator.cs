using Controllers.Pools;
using UnityEngine;

namespace Weapons.SpecificWeapons
{
    public class MechanicalMeatSeperator : Weapon
    {
        private float _attackInterval,  _attackTimer;
        private Vector3 _direction;

        private void Start()
        {
            SetStats();
        }

        private void FixedUpdate()
        {
            _attackTimer -= Time.deltaTime;
            if (_attackTimer <= 0) LaunchSawBlade();
        }
    
        private void SetStats()
        {
            _attackInterval = stats[weaponLevel].rateOfFire;
            _attackTimer = _attackInterval;
        }

        private void LaunchSawBlade()
        {
            _attackTimer = _attackInterval;
            var saw = ProjectilePoolManager2.poolProj.projPools[1].GetPooledGameObject();
            saw.transform.position = transform.position;
            saw.SetActive(true);
        }
    }
}
