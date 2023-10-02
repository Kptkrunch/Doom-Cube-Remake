using GenUtilsAndTools;
using UnityEngine;

namespace Weapons.SpecificWeapons
{
    public class MeleeWeapon : Weapon
    {
        public EnemyDamager enemyDamager;
        public GameObject theWeapon;
        private bool _weaponCanBeDrawn;
        private float _attackDuration, _attackInterval, _attackTimer, _direction;

        private void Start()
        {
            SetStats();
        }

        private void Update()
        {
            if (_weaponCanBeDrawn)
            {
                _attackTimer -= Time.deltaTime;
                if (_attackTimer <= 0)
                {
                    if (Input.GetAxisRaw("Horizontal") != 0)
                    {
                        if (Input.GetAxisRaw("Horizontal") > 0)
                        {
                            enemyDamager.transform.rotation = Quaternion.identity;
                        }
                        else
                        {
                            enemyDamager.transform.rotation = Quaternion.Euler(0f, 0f, 180f);
                        }
                        
                        _weaponCanBeDrawn = false;
                        _attackTimer = _attackInterval;
                        theWeapon.gameObject.SetActive(true);
                    }
                
                }
            }


            if (!_weaponCanBeDrawn)
            {
                _attackTimer -= Time.deltaTime;
                if (_attackTimer <= 0)
                {
                    _weaponCanBeDrawn = true;
                    _attackTimer = _attackDuration;
                    theWeapon.gameObject.SetActive(false);
                }
            }
        }

        private void SetStats()
        {
            enemyDamager.damage = stats[weaponLevel].damage;
            _attackDuration = stats[weaponLevel].duration;
            _attackInterval = stats[weaponLevel].rateOfFire;
            _attackTimer = _attackInterval;
            _weaponCanBeDrawn = true;

        }
    }
}
