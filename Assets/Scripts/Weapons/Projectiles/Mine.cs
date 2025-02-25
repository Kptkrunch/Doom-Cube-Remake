using Controllers;
using Controllers.Pools;
using Damagers;
using UnityEngine;
using Weapons.SpecificWeapons;

namespace Weapons.Projectiles
{
    public class Mine : Projectile
    {
        public float damage, expRadius;

        private void Awake()
        {
            _fuseTimer = pd.stats.fuseTime;
            _lifeTimer = pd.stats.lifeTime;
            damage = pd.stats.damage;
            expRadius = pd.stats.size;
        }

        private void FixedUpdate()
        {
            MaybeHasFuseTime();
            MaybeHasLifetime();
        }

        protected override void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Enemy")) Detonate();
        }

        private void Detonate()
        {
            var explosion = MineSlayer.Instance.explosionPool.GetPooledGameObject();
            var damager = explosion.GetComponent<EExplosionDamager>();
            damager.damage = damage;
            damager.blastRadiusCollider.radius = expRadius;
            explosion.gameObject.transform.position = transform.position;
            explosion.SetActive(true);
            GenericShakeController.Instance.ShakeWeakStrongViolent("weak", transform);
            parent.gameObject.SetActive(false);
        }

        private void MaybeHasFuseTime()
        {
            if (!it.hasFuse) return;
            
            _fuseTimer -= Time.deltaTime;
            if (_fuseTimer <= 0)
            {
                _fuseTimer = pd.stats.fuseTime;
                if (it.explodes) Detonate();
                parent.gameObject.SetActive(false);
            }
        }
    }
}