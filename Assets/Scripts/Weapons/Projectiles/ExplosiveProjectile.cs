using Controllers;
using Controllers.Pools;
using Damagers;
using MoreMountains.Feedbacks;
using UnityEngine;

namespace Weapons.Projectiles
{
    public class ExplosiveProjectile : Projectile
    {
        public int expIndex, damage, radius, initBounces, vertPower, horPower;
        private float _bounceHardInterval, _disableTimer;
        private MMF_Player _player;

        public ExplosiveProjectile(float bounceHardInterval)
        {
            _bounceHardInterval = bounceHardInterval;
        }

        private void Awake()
        {
            pd.stats.bounces = initBounces;
            _bounceHardInterval = 2f;
            _disableTimer = 2f;
            // bounceInterval = _bounceHardInterval;
            // bounceTimer = bounceInterval;
    }

        private void FixedUpdate()
        {
            if (it.doesBounce) MaybeBounceHandler();
            if (it.delayDisable) MaybeDelayDisable();
        }

        public override void OnEnable()
        {
            base.OnEnable();
            Reset();
            // rb2d.gravityScale = 1;
            // rb2d.velocity = new Vector2(Random.Range(-horPower, horPower), vertPower + 1);
            if (it.doesBounce)
            {
                // bounceInterval = _bounceHardInterval;
                // bounceTimer = Random.Range(bounceInterval * 0.5f, bounceInterval * 1.75f);
            }
        }

        private void OnDisable()
        {
            if (it.disableAfterBounces) Detonate();
        }

        private void HardStop()
        {
            // rb2d.velocity = new Vector2(0f, 0f).normalized;
            // rb2d.gravityScale = 0f;
            it.doesBounce = false;
        }

        private void Detonate()
        {
            var exp = ProjectilePoolManager2.poolProj.projPools[expIndex].GetPooledGameObject();
            var damager = exp.GetComponent<EExplosionDamager>();
            damager.damage = damage;
            damager.blastRadiusCollider.radius = radius;
            damager.gameObject.transform.position = transform.position;
            damager.gameObject.SetActive(true);
            gameObject.SetActive(false);
            juiceManager.TriggerFeedback(GenericJuiceManager.FeedbackType.Death);
        }

        private void Reset()
        {
            pd.stats.bounces = initBounces;
            _bounceHardInterval = 2f;
            // bounceInterval = _bounceHardInterval;
            // bounceTimer = bounceInterval;
            // if (rb2d.gravityScale == 0) rb2d.gravityScale = 1;
            // rb2d.velocity = new Vector2(0f, 0f).normalized;
            it.delayDisable = false;
            it.doesBounce = true;
            _disableTimer = 2f;
        }

        private void MaybeDelayDisable()
        {
            if (it.delayDisable) _disableTimer -= Time.deltaTime;
            if (_disableTimer <= 0) Detonate();
        }

        private void MaybeBounceHandler()
        {
            // bounceTimer -= Time.deltaTime;
            // switch (bounceTimer)
            // {
            //     case <= 0:
            //     {
            //         var velocity = rb2d.velocity;
            //         velocity = new Vector2(velocity.x, velocity.y).normalized;
            //         rb2d.velocity = velocity;
            //         bounceInterval *= 0.8f;
            //         bounceTimer = bounceInterval;
            //         rb2d.AddForce(new Vector2(velocity.x * 0.8f, lobHeight * .75f), ForceMode2D.Impulse);
            //         bounces--;
            //
            //         if (bounces <= 0)
            //         {
            //             HardStop();
            //             if (it.disableAfterBounces) gameObject.SetActive(false);
            //             it.delayDisable = true;
            //         }
            //
            //         break;
            //     }
            // }
        }
    }
}