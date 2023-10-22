using Damagers;
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
            auraParticles.localScale = Vector3.one * stats[weaponLevel].size;
            auraCollider.gameObject.transform.localScale = Vector3.one * stats[weaponLevel].size;

        }

        public override void UpdateWeapon()
        {
            WeaponLevelUp();

            // update aura damage
            auraDamager.damage = stats[weaponLevel].damage;
        
            // increase radius of collider and aura
            auraCollider.gameObject.transform.localScale = Vector3.one * stats[weaponLevel].size;
            auraParticles.transform.localScale = Vector3.one * stats[weaponLevel].size;        
        
            // reduce time between damage tics
            auraDamager.damageInterval = 1f / stats[weaponLevel].rateOfFire;
        }
    }
}
