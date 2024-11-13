using Controllers.Pools;
using Damagers;
using UnityEngine;

namespace Weapons.Projectiles
{
    public class Mine : Projectile
    {
        public int expIndex;
        public bool hasFuse;
        public float damage, expRadius;
        public float fuseTimer = 3;
        private float _fuseTimer;

        private void Awake()
        {
            _fuseTimer = fuseTimer;
            damage = pd.stats.damage;
            expRadius = pd.stats.size;
        }

        private void FixedUpdate()
        {
            if (hasFuse)
            {
                _fuseTimer -= Time.deltaTime;
                if (_fuseTimer <= 0)
                {
                    _fuseTimer = fuseTimer;
                    if (it.explodes) Detonate();
                    parent.gameObject.SetActive(false);
                }
            }
        }

        protected override void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Enemy")) Detonate();
        }

        private void Detonate()
        {
            var explosion = ProjectilePoolManager2.poolProj.projPools[expIndex].GetPooledGameObject();
            var damager = explosion.GetComponent<EExplosionDamager>();
            damager.damage = damage;
            damager.blastRadiusCollider.radius = expRadius;
            Debug.Log(damager.name);
            explosion.gameObject.transform.position = transform.position;
            explosion.SetActive(true);
            parent.gameObject.SetActive(false);
        }
    }
}