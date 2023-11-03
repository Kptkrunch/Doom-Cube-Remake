using UnityEngine;

namespace Weapons.Projectiles
{
    public class BouncingProjectile : Projectile
    {
        
        public Rigidbody2D rb2d;
        public float bounceInterval;
        private float _bounceTimer;
        
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
                    _bounceTimer -= Time.deltaTime;
                    if (_bounceTimer <= 0)
                    {
                        var velocity = rb2d.velocity;
                        velocity = new Vector2(velocity.x, velocity.y).normalized;
                        rb2d.velocity = velocity;
                        bounceInterval *= 0.8f;
                        _bounceTimer = bounceInterval;
                        rb2d.AddForce(new Vector2(velocity.x * 0.8f, pd.stats.lobHeight * .75f), ForceMode2D.Impulse);
                        pd.stats.bounces--;
                
                        if (pd.stats.bounces <= 0)
                        {
                            rb2d.velocity = Vector2.zero;
                            rb2d.gravityScale = 0f;
                            if (it.disableAfterBounces) gameObject.SetActive(true);
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
                rb2d.gravityScale = 1;
                rb2d.velocity = new Vector2(
                    Random.Range(-pd.stats.lobDistance, pd.stats.lobDistance), 
                    pd.stats.lobHeight + 1);
            }
            
            if (it.doesBounce)
            {
                _bounceTimer = Random.Range(_bounceTimer * 0.5f, _bounceTimer * 1.75f);
            }

            if (it.moveUseVelocity)
            {
                if (!it.moveUseTranslate) rb2d.velocity = pd.stats.direction * (pd.stats.movSpeed * Time.deltaTime);
            }
        }
        
        private void OnDisable()
        {
            rb2d.velocity = new Vector2(0f, 0f);
        }
    }
}
