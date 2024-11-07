using Controllers.Pools;
using Damagers;
using UnityEngine;

namespace Weapons.Projectiles
{
    public class ExplosiveProjectile : Projectile
    {
        public int expIndex, damage, radius, initBounces, vertPower, horPower;
        private float _disableTimer;

        private void Awake()
        {
            pd.stats.bounces = initBounces;
            _disableTimer = 2f;
        }

        private void FixedUpdate()
        {
            // if (it.doesBounce) MaybeBounceHandler();
            if (it.delayDisable) MaybeDelayDisable();
        }

        public override void OnEnable()
        {
            Reset();
            Debug.Log("enabled");
            if (it.isLobbed) Debug.Log("lobbed");
        }

        private void OnDisable()
        {
            if (it.disableAfterBounces) Detonate();
            Debug.Log("after detonate");
        }

        private void HardStop()
        {
            it.doesBounce = false;
        }

        private void Detonate()
        {
            var exp = ProjectilePoolManager.poolProj.projPools[3].GetPooledGameObject();
            var damager = exp.GetComponent<EExplosionDamager>();
            damager.damage = damage;
            damager.blastRadiusCollider.radius = radius;
            damager.gameObject.transform.position = transform.position;
            damager.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }

        private void Reset()
        {
            pd.stats.bounces = initBounces;
            it.delayDisable = false;
            it.doesBounce = true;
            _disableTimer = 2f;
        }

        private void MaybeDelayDisable()
        {
            if (it.delayDisable) _disableTimer -= Time.deltaTime;
            if (_disableTimer <= 0) Detonate();
        }
    }
}