using Damagers;
using GenUtilsAndTools;
using UnityEngine;

namespace Weapons.SpecificWeapons
{
    // same as melee weapon, but accepts a different damager that does dot damage
    public class BurnWeaponMelee : Weapon
    {
        public EDotDamager eDotDamager; 
        public GrowShrinkObj weaponScaler;
        public ProjFrameWithTimer weaponFrame;
        
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
                    if (Input.GetAxisRaw("Horizontal") > 0)
                    {
                        weaponFrame.transform.localScale =
                            new Vector2(1, transform.localScale.y);
                        eDotDamager.transform.rotation = Quaternion.identity;
                        _weaponCanBeDrawn = false;
                    } else if (Input.GetAxisRaw("Horizontal") < 0)
                    {
                        weaponFrame.transform.localScale =
                            new Vector2(-1, transform.localScale.y);
                        eDotDamager.transform.rotation = Quaternion.Euler(0f, 0f, 180f);
                        _weaponCanBeDrawn = false;
                    }
                    
                    _attackTimer = _attackDuration;
                    weaponFrame.gameObject.SetActive(true);
                }
            }
            
            if (!_weaponCanBeDrawn)
            {
                _attackTimer -= Time.deltaTime;
                if (_attackTimer <= 0)
                {
                    _weaponCanBeDrawn = true;
                    _attackTimer = _attackInterval;
                    weaponFrame.gameObject.SetActive(false);
                }
            }
        }

        private void SetStats()
        {
            _attackDuration = stats[weaponLevel].duration;
            _attackInterval = stats[weaponLevel].cdr;
            _attackTimer = _attackInterval;
            eDotDamager.damageInterval = stats[weaponLevel].rateOfFire;
            eDotDamager.damage = stats[weaponLevel].damage;
            _weaponCanBeDrawn = true;
            if (weaponFrame)
            {
                weaponFrame.coolDownTimer = stats[weaponLevel].cdr;
                weaponFrame.activeInterval = stats[weaponLevel].duration;
            }
            if (weaponScaler)
            {
                weaponScaler.staySizeInterval = weaponFrame.activeInterval * .6f;
                weaponScaler.maxSize = Vector3.one;
                weaponScaler.growShrinkSpeed = stats[weaponLevel].projSpeed;
            }
        }
        
        public override void UpdateWeapon()
        {
            WeaponLevelUp();

            _attackInterval *= stats[weaponLevel].cdr;
            _attackDuration *= stats[weaponLevel].duration;
            // increase projectile size and growth speed
            // match growth interval to weapon frame interval
            weaponFrame.activeInterval = stats[weaponLevel].duration;
            weaponFrame.coolDownTimer = stats[weaponLevel].cdr;
            // increase projectile size and growth speed
            weaponScaler.staySizeInterval *= stats[weaponLevel].duration * .6f;
            weaponScaler.maxSize.x *= stats[weaponLevel].size;
            weaponScaler.growShrinkSpeed *= stats[weaponLevel].projSpeed;
            
            // increase radius
            var newScale = new Vector3(1f, stats[weaponLevel].size, 1f);
            transform.localScale = newScale;
            // increase damage and damage interval
            eDotDamager.damageInterval *= stats[weaponLevel].cdr;
            eDotDamager.damage *= stats[weaponLevel].damage;

        }
    }
}
