using UnityEngine;

namespace Weapons.Projectiles
{
    public class MeatSaw : Projectile
    {
        private Vector3 _growSawScale;
        private bool _isGrowing, _triggered;
        private float _growSpeed, _spinTimer;
        private const float SpinInterval = 3f;

        private void Start()
        {
            SetStats();
        }

        private void FixedUpdate()
        {
            if (_isGrowing)
            {
                _spinTimer -= Time.deltaTime;
            }
            SpoolUpAndFire();
        }

        private void OnEnable()
        {
            LobSaw();
        }
    
        private void OnDisable()
        {
            bounces = 2;
            _isGrowing = false;
            _triggered = false;
            transform.localScale = Vector3.one;
            _spinTimer = SpinInterval;
        }

        public void SpoolUpAndFire()
        {
            switch (bounces)
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
                        moveSpeed = 4f;
                        rb2d.gameObject.transform.position += direction * (moveSpeed * Time.deltaTime);
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
                direction = new Vector3(-1, 0, 0).normalized;
            } else if (randomizeTransform.x >= 0)
            {
                direction = new Vector3(1, 0, 0).normalized;
            }
        }

        private void SetStats()
        {
            
            bounceInterval = hardBoundeInterval;
            bounceTimer = bounceInterval;
            if (lobDistance < 1) lobDistance = 5;
            if (lobHeight < 1) lobHeight = 5;
            _growSawScale = new Vector3(2f, 2f, transform.localScale.z);
            _growSpeed = 1f * Time.deltaTime;
            _spinTimer = SpinInterval;
        }
        private void StopMoving()
        {
            rb2d.velocity = Vector3.zero;
            moveSpeed = 0f;
        }

        private void LobSaw()
        {
            if (it.isLobbed)
            {
                rb2d.velocity = new Vector2(Random.Range(-lobHeight, lobHeight), Random.Range(0, lobHeight + 1f));
            }
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
