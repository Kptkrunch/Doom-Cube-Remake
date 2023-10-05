using Damagers;
using GenUtilsAndTools;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Weapons
{
    public class BombOrNade : Projectile
    {
        public EExplosionDamager eExplosionDamager;
        private void Update()
        {
            if (doesBounce)
            {
                bounceTimer -= Time.deltaTime;
                if (bounceTimer <= 0)
                {
                    var velocity = rb2d.velocity;
                    velocity = new Vector2(velocity.x, velocity.y).normalized;
                    rb2d.velocity = velocity;
                    bounceInterval *= 0.8f;
                    bounceTimer = bounceInterval;
                    rb2d.AddForce(new Vector2(velocity.x * 0.8f, lobHeight * .75f), ForceMode2D.Impulse);
                    bounces--;

                    if (bounces <= 0)
                    {
                        eExplosionDamager.Detonate();
                        rb2d.velocity = Vector2.zero;
                        rb2d.gravityScale = 0f;
                        Destroy(gameObject, bounceTimer * 2);
                    }
                }
            }
        }
        
        private void OnEnable()
        {
            if (isLobbed)
            {
                rb2d.velocity = new Vector2(Random.Range(-lobDistance, lobDistance), lobHeight);
            }
            
            if (doesBounce)
            {
                bounceTimer = Random.Range(bounceTimer * 0.5f, bounceTimer * 1.75f);
            }
        }
    }
}
