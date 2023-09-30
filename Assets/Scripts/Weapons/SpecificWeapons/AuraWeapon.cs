using System.Collections;
using System.Collections.Generic;
using EffectsTools;
using UnityEngine;
using Weapons;

public class AuraWeapon : Weapon
{
    public EAuraDamager enemyDamager;
    public CircleCollider2D auraCollider;
    public Transform auraParticles;
    
    public override void UpdateWeapon()
    {
        WeaponLevelUp();

        // update projectile damage, add more for more projectiles
        enemyDamager.damage = stats[weaponLevel].damage;
        
        // increase radius
        auraCollider.radius *= stats[weaponLevel].range;
        auraParticles.transform.localScale = Vector3.one * stats[weaponLevel].range;        
        
        // increase duration and reduce cooldown
        enemyDamager.damageInterval *= stats[weaponLevel].cdr;
    }
}
