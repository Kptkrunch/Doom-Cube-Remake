using System;
using System.Collections;
using Controllers.Pools;
using Damagers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Weapons.Projectiles
{
    public class BouncingProjectile : Projectile
    {
        
        public Rigidbody2D rb2d;
        protected float BounceTimer, BounceInterval, Bounces;

        private void Start()
        {
            BounceInterval = pd.stats.bounceInterval;
            BounceTimer = BounceInterval;
            Bounces = pd.stats.bounces;
        }

        private void FixedUpdate()
        {
            if (!it.isLobbed)
            {
                var transform1 = transform;
                transform1.position += transform1.up * (pd.stats.movSpeed * Time.deltaTime);
            } 
            
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
                        
                        if (Bounces <= 0)
                        {
                            Debug.Log("done bouncing");

                            rb2d.velocity = Vector2.zero;
                            rb2d.gravityScale = 0f;
                            if (it.disableAfterBounces) gameObject.SetActive(false);
                            it.delayDisable = true;
                        }
                    }
                    break;
                }
            }
        }

        public override void OnEnable()
        {
            if (it.isLobbed)
            {
                Debug.Log("inside lobbed");

                rb2d.gravityScale = 1;
                rb2d.velocity = new Vector2(
                    Random.Range(-pd.stats.lobDistance, pd.stats.lobDistance), 
                    pd.stats.lobHeight + 1);
                Debug.Log(rb2d.velocity);

            }
            
            if (it.doesBounce)
            {
                BounceTimer = Random.Range(BounceTimer * 0.5f, BounceTimer * 1.75f);
                Debug.Log(BounceTimer);
            }

            if (it.moveUseVelocity)
            {
                if (!it.moveUseTranslate) rb2d.velocity = pd.stats.direction * (pd.stats.movSpeed * Time.deltaTime);
            }
        }
        
        private void OnDisable()
        {
            if (it.itExplodes)
            {
                Debug.Log("explodes");

                var exp = ProjectilePoolManager2.poolProj.projPools[pd.eid].GetPooledGameObject();
                exp.transform.position = transform.position;
                var damager = exp.GetComponent<EnemyDamager>();
                damager.damage = pd.stats.damage;
                damager.GetComponent<CircleCollider2D>().radius = pd.stats.size;
                exp.SetActive(true);
                
            }
            rb2d.velocity = new Vector2(0f, 0f);
            BounceInterval = pd.stats.bounceInterval;
            BounceTimer = BounceInterval;
            Bounces = pd.stats.bounces;
        }
    }
}
