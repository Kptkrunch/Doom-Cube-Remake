using Controllers.Pools;
using Damagers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Weapons.Projectiles
{
    public class BouncingProjectile : Projectile
    {
        public Rigidbody2D rb2d;
        protected float BounceTimer, BounceInterval, Bounces, LifeTimer;
        protected bool RestoreLob;

        private void Start()
        {
            LifeTimer = pd.stats.lifeTime;
            if (it.isLobbed) RestoreLob = true;
            SetStats();
        }

        private void FixedUpdate()
        {
            switch (it.doesBounce)
            {
                case true:
                {
                    BounceTimer -= Time.deltaTime;
                    Debug.Log("in bounce logic");

                    if (BounceTimer <= 0)
                    {
                        var velocity = rb2d.velocity;
                        velocity = new Vector2(velocity.x, velocity.y).normalized;
                        rb2d.velocity = velocity;
                        BounceInterval *= 0.8f;
                        BounceTimer = BounceInterval;
                        rb2d.AddForce(new Vector2(velocity.x * 0.8f, pd.stats.lobHeight * .75f), ForceMode2D.Impulse);
                        Bounces--;
                    }

                    break;
                }
            }

            if (it.isLobbed)
            {
                it.isLobbed = false;
                LobProjectile();
            }

            if (Bounces <= 0)
            {
                Bounces = pd.stats.bounces;
                gameObject.SetActive(false);
            }
        }

        public override void OnEnable()
        {
            if (it.doesBounce) SetBouncTimer();
        }

        private void OnDisable()
        {
            if (RestoreLob) it.isLobbed = true;
            SetStats();
            if (it.explodes) MaybeExplode();
        }

        private void SetStats()
        {
            pd.stats.lifeTime = LifeTimer;
            rb2d.velocity = new Vector2(0f, 0f);
            BounceInterval = pd.stats.bounceInterval;
            BounceTimer = BounceInterval;
            Bounces = pd.stats.bounces;
        }

        private void LobProjectile()
        {
            rb2d.velocity = new Vector2(Random.Range(-pd.stats.lobDistance, pd.stats.lobDistance), pd.stats.lobHeight) *
                            (pd.stats.movSpeed * Time.deltaTime);
        }

        private void SetBouncTimer()
        {
            BounceTimer = Random.Range(BounceTimer * 0.5f, BounceTimer * 1.75f);
        }

        protected void MaybeExplode()
        {
            var exp = ProjectilePoolManager.poolProj.projPools[pd.eid].GetPooledGameObject();
            var damager = exp.GetComponent<EnemyDamager>();

            exp.transform.position = transform.position;
            damager.damage = pd.stats.damage;
            damager.GetComponent<CircleCollider2D>().radius = pd.stats.size;
            exp.SetActive(true);
        }
    }
}