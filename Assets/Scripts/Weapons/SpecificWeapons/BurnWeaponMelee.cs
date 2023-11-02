using Damagers;
using GenUtilsAndTools;
using UnityEngine;
using Weapons.WeaponModifiers;

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

            switch (_weaponCanBeDrawn)
            {
                case false:
                {
                    _attackTimer -= Time.deltaTime;
                    if (_attackTimer <= 0)
                    {
                        _weaponCanBeDrawn = true;
                        _attackTimer = _attackInterval;
                        weaponFrame.gameObject.SetActive(false);
                    }

                    break;
                }
            }
        }

        private void SetStats()
        {
            _attackDuration = stats.weaponLvls[stats.lvl].duration;
            _attackInterval = stats.weaponLvls[stats.lvl].coolDown;
            _attackTimer = _attackInterval;
            eDotDamager.damageInterval = stats.weaponLvls[stats.lvl].rateOfFire;
            eDotDamager.damage = stats.weaponLvls[stats.lvl].damage;
            _weaponCanBeDrawn = true;
            if (weaponFrame)
            {
                weaponFrame.coolDownTimer = stats.weaponLvls[stats.lvl].coolDown;
                weaponFrame.activeInterval = stats.weaponLvls[stats.lvl].duration;
            }

            if (!weaponScaler) return;
            weaponScaler.staySizeInterval = weaponFrame.activeInterval * .6f;
            weaponScaler.maxSize = Vector3.one;
            weaponScaler.growShrinkSpeed = stats.weaponLvls[stats.lvl].speed;
        }
        
        public override void UpdateWeapon()
        {
            WeaponLevelUp();

            _attackInterval = stats.weaponLvls[stats.lvl].rateOfFire;
            _attackDuration = stats.weaponLvls[stats.lvl].duration;
            // increase projectile size and growth speed
            // match growth interval to weapon frame interval
            weaponFrame.activeInterval = stats.weaponLvls[stats.lvl].duration;
            weaponFrame.coolDownTimer = stats.weaponLvls[stats.lvl].coolDown;
            // increase projectile size and growth speed
            weaponScaler.staySizeInterval = stats.weaponLvls[stats.lvl].duration * .6f;
            weaponScaler.maxSize.x = stats.weaponLvls[stats.lvl].range;
            weaponScaler.growShrinkSpeed = stats.weaponLvls[stats.lvl].speed;
            
            // increase radius
            transform.localScale = Vector3.one * stats.weaponLvls[stats.lvl].range;
            // increase damage and damage interval
            eDotDamager.damageInterval = 1f / stats.weaponLvls[stats.lvl].coolDown;
            eDotDamager.damage = stats.weaponLvls[stats.lvl].damage;

        }
    }
}
