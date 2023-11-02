using UnityEngine;
using Weapons.Projectiles;
using Random = UnityEngine.Random;

namespace Weapons.SpecificWeapons
{
    public class RadarWeapon : Weapon
    {
        public Projectile projectile;
        
        private float _weaponRange, _fireTimer, _fireInterval;
        [SerializeField] private LayerMask enemyLayer;

        private void Start()
        {
            SetStats();
        }

        private void Update()
        {
            _fireTimer -= Time.deltaTime;
            if (_fireTimer <= 0)
            {
                _fireTimer = _fireInterval;
                Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, _weaponRange, enemyLayer);
                if (enemies.Length > 0)
                {
                    for (var i = 0; i < stats.weaponLvls[stats.lvl].ammo; i++)
                    {
                        var targetPosition = enemies[Random.Range(0, enemies.Length)].transform.position;

                        var position = transform.position;
                        var targetDirection = targetPosition - position;
                        var angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
                        angle -= 90;
                        projectile.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

                        Instantiate(projectile, position, projectile.transform.rotation).gameObject.SetActive(true);
                    }
                } 
            }
        }
        
        private void SetStats()
        {
            projectile.enemyDamager.damage = stats.weaponLvls[stats.lvl].damage;
            projectile.moveSpeed = stats.weaponLvls[stats.lvl].speed;
            _fireInterval = stats.weaponLvls[stats.lvl].rateOfFire;
            _fireTimer = _fireInterval;
            _weaponRange = stats.weaponLvls[stats.lvl].range;
            projectile.numberOfPenetrates = stats.weaponLvls[stats.lvl].duration;
        }
        
        public override void UpdateWeapon()
        {
            WeaponLevelUp();

            // update projectile damage and speed
            projectile.enemyDamager.damage = stats.weaponLvls[stats.lvl].damage;
            projectile.moveSpeed = stats.weaponLvls[stats.lvl].speed;
            projectile.numberOfPenetrates = stats.weaponLvls[stats.lvl].duration;
            // increase detection range
            _weaponRange = stats.weaponLvls[stats.lvl].range;
            projectile.transform.localScale = Vector3.one * stats.weaponLvls[stats.lvl].range;
            // reduce time between volleys
            _fireInterval = 3f / stats.weaponLvls[stats.lvl].rateOfFire;
        }
    }
}
