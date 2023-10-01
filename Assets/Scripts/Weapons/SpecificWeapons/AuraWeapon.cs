using GenUtilsAndTools;
using UnityEngine;

namespace Weapons.SpecificWeapons
{
    public class AuraWeapon : Weapon
    {
        public EAuraDamager auraDamager;
        public CircleCollider2D auraCollider;
        public Transform auraParticles;

        public override void UpdateWeapon()
        {
            WeaponLevelUp();

            // update projectile damage, add more for more projectiles
            auraDamager.damage = stats[weaponLevel].damage;
        
            // increase radius
            auraCollider.radius *= stats[weaponLevel].range;
            auraParticles.transform.localScale = Vector3.one * stats[weaponLevel].range;        
        
            // increase duration and reduce cooldown
            auraDamager.damageInterval *= stats[weaponLevel].cdr;
        }
    }
}
