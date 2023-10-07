using System;
using Damagers;
using GenUtilsAndTools;
using UnityEngine;

namespace Weapons.SpecificWeapons
{
    public class AuraWeapon : Weapon
    {
        public EAuraDamager auraDamager;
        [SerializeField] private CircleCollider2D auraCollider;
        [SerializeField] private Transform auraParticles;

        private void Start()
        {
            SetStats();
        }

        private void SetStats()
        {
            auraDamager.damage = stats[weaponLevel].damage;
            auraDamager.damageInterval = stats[weaponLevel].rateOfFire;
            auraParticles.localScale = Vector3.one * stats[weaponLevel].range;
        }

        public override void UpdateWeapon()
        {
            WeaponLevelUp();

            // update aura damage
            auraDamager.damage = stats[weaponLevel].damage;
        
            // increase radius of collider and aura
            auraCollider.gameObject.transform.localScale = Vector3.one * stats[weaponLevel].range;
            auraParticles.transform.localScale = Vector3.one * stats[weaponLevel].range;        
        
            // reduce time between damage tics
            auraDamager.damageInterval *= stats[weaponLevel].cdr;
        }
    }
}
