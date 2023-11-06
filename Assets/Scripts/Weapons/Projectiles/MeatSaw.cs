using UnityEngine;

namespace Weapons.Projectiles
{
    public class MeatSaw : BouncingProjectile
    {
        private Vector3 _growSawScale;
        private bool _isGrowing, _triggered;
        private float _growSpeed, _spinTimer, _lifeTimer;
        private const float SpinInterval = 3f;
        private Vector3 _direction;

        private void Start()
        {
            _lifeTimer = pd.stats.lifeTime;
            SetStats();
        }

        private void FixedUpdate()
        {
            
            MaybeRotate(rb2d);

            if (it.hasLifetime)
            {
                pd.stats.lifeTime -= Time.deltaTime;
                if (pd.stats.lifeTime <= 0) parent.SetActive(false);
            }
            
            if (_isGrowing)
            {
                _spinTimer -= Time.deltaTime;
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

                        SpoolUpAndFire();
                    }
                    break;
                }
            }
        }

        public override void OnEnable()
        {
            LobSaw();
        }
    
        private void OnDisable()
        {
            SetStats();
        }

        public void SpoolUpAndFire()
        {
            switch (Bounces)
            {
                case <= 0:
                {
                    _isGrowing = true;
                    it.doesRotate = true;
                    StopMoving();
                    GrowSaw();
                    if (_spinTimer <= 0)
                    {
                        if (!_triggered) MaybeGoLeftOrRight();
                        _triggered = true;
                        pd.stats.movSpeed = 4f;
                        rb2d.gameObject.transform.position += _direction * (pd.stats.movSpeed * Time.deltaTime);
                    }

                    break;
                }
            }
        }
    
        private void MaybeGoLeftOrRight()
        {
            Transform transform1;
            var randomizeTransform = new Vector3(Random.Range(-1, 1), (transform1 = transform).position.y, transform1.position.z);
            if (randomizeTransform.x < 0)
            {
                _direction = new Vector3(-1, 0, 0).normalized;
            } else if (randomizeTransform.x >= 0)
            {
                _direction = new Vector3(1, 0, 0).normalized;
            }
        }

        private void SetStats()
        {
            _growSawScale = new Vector3(2f, 2f, transform.localScale.z);
            _growSpeed = 1f * Time.deltaTime;
            pd.stats.lifeTime = _lifeTimer;
            _isGrowing = false;
            _triggered = false;
            it.doesRotate = false;
            _spinTimer = SpinInterval;
            rb2d.velocity = new Vector2(0f, 0f);
            transform.localScale = Vector3.one;
            BounceInterval = pd.stats.bounceInterval;
            BounceTimer = BounceInterval;
            Bounces = pd.stats.bounces;
        }
        private void StopMoving()
        {
            rb2d.velocity = Vector3.zero;
            pd.stats.movSpeed = 0f;
        }

        private void LobSaw()
        {
            if (it.isLobbed)
            {
                Debug.Log("inside lobbed saw");

                rb2d.gravityScale = 1;
                rb2d.velocity = new Vector2(
                    Random.Range(-pd.stats.lobDistance, pd.stats.lobDistance), 
                    pd.stats.lobHeight + 1);
                Debug.Log(rb2d.velocity);            }
        }

        private void GrowSaw()
        {
            switch (_isGrowing)
            {
                case true:
                {
                    var localScale = transform.localScale;
                    localScale = Vector3.Lerp(localScale, _growSawScale, _growSpeed);
                    transform.localScale = localScale;
                    break;
                }
            }
        }
    }
}
