using Controllers.Pools;

namespace Weapons.SpecificWeapons
{
    public class LobbedWeapon : PrefabBasedWeapon
    {
        private float _attackInterval, _attackTimer;
        
        private void Start()
        {
            SetStats();
        }

        private void SetStats()
        {
            _attackInterval = stats.weaponLvls[stats.lvl].rateOfFire;
            _attackTimer = _attackInterval;
        }

        public override void UpdateWeapon()
        {
            _attackInterval = stats.weaponLvls[stats.lvl].rateOfFire;
            stats.weaponLvls[stats.lvl].ammo = stats.weaponLvls[stats.lvl].ammo;
        }

        protected override void Fire()
        {
            _attackTimer = _attackInterval;
            for (var i = 0; i < stats.weaponLvls[stats.lvl].ammo; i++)
            {
                var bomb = ProjectilePoolManager.poolProj.projPools[stats.pid].GetPooledGameObject();
                bomb.transform.position = transform.position;
                bomb.gameObject.SetActive(true);
            }
        }
    }
}
