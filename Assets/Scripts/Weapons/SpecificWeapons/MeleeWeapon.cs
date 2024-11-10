using Damagers;
using GenUtilsAndTools;
using UnityEngine;

namespace Weapons.SpecificWeapons
{
    public class MeleeWeapon : Weapon
    {
        public EnemyDamager enemyDamager;

        protected bool CanFire;
        protected float AttackDuration, Cooldown, RateOfFire, Direction;

        private void Start()
        {
            SetStats();
        }

        private void SetStats()
        {
            CanFire = true;
            AttackDuration = stats.weaponLvls[stats.lvl].duration;
            RateOfFire = stats.weaponLvls[stats.lvl].coolDown;
            enemyDamager.damage = stats.weaponLvls[stats.lvl].damage;
        }
    }
}