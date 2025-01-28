using System;
using Controllers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Weapons.Projectiles
{
    public class MeatSaw : BouncingProjectile
    {
        public GenericJuiceManager juiceManager;
        private Vector3 _growSawScale;
        private bool _isGrowing, _triggered;
        private float _growSpeed, _spinTimer;
        private const float SpinInterval = 2f;
        private Vector3 _direction;
        private float _sawSpeed;

        private void Start()
        {
            LifeTimer = pd.stats.lifeTime;
            _sawSpeed = pd.stats.movSpeed;
            pd.stats.movSpeed = 0f;
            if (it.isLobbed) RestoreLob = true;
            SetStats();
        }

        private void FixedUpdate()
        {
            if (it.isLobbed)
            {
                it.isLobbed = false;
                LobSaw();
            }

            switch (it.doesBounce)
            {
                case true:
                {
                    BounceTimer -= Time.deltaTime;
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

            if (_isGrowing)
            {
                _spinTimer -= Time.deltaTime;
                // play the saw sound only once it starts spinning.
                GrowSaw();
            }

            MaybeRotate(rb2d);

            if (it.hasLifetime)
            {
                pd.stats.lifeTime -= Time.deltaTime;
                if (pd.stats.lifeTime <= 0) parent.SetActive(false);
            }
        }

        private void OnDisable()
        {
            pd.stats.movSpeed = _sawSpeed;
            juiceManager.StopFeedback(GenericJuiceManager.FeedbackType.Idle);
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
                    GrowSaw();
                    StopMoving();
                    if (_spinTimer <= 0)
                    {
                        if (!_triggered) MaybeGoLeftOrRight();
                        _triggered = true;
                        pd.stats.movSpeed = 4f;
                        rb2d.gameObject.transform.position += _direction * (pd.stats.movSpeed * Time.deltaTime);
                    }
                    if (!juiceManager.idleFeedbacks.FeedbacksList[0].IsPlaying)
                    {
                        juiceManager.TriggerFeedback(GenericJuiceManager.FeedbackType.Idle);
                    }
                    break;
                }
            }
        }

        private void MaybeGoLeftOrRight()
        {
            Transform transform1;
            var randomizeTransform = new Vector3(Random.Range(-1, 1), (transform1 = transform).position.y,
                transform1.position.z);
            if (randomizeTransform.x < 0)
                _direction = new Vector3(-1, 0, 0).normalized;
            else if (randomizeTransform.x >= 0) _direction = new Vector3(1, 0, 0).normalized;
        }

        private void SetStats()
        {
            if (RestoreLob) it.isLobbed = true;
            _growSawScale = new Vector3(2f, 2f, transform.localScale.z);
            _growSpeed = 1f * Time.deltaTime;
            pd.stats.lifeTime = LifeTimer;
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
            rb2d.gravityScale = 0;
            rb2d.velocity = Vector2.zero;
            pd.stats.movSpeed = 0f;
        }

        private void LobSaw()
        {
            rb2d.gravityScale = 1;
            rb2d.velocity = new Vector2(
                Random.Range(-pd.stats.lobDistance, pd.stats.lobDistance),
                pd.stats.lobHeight);
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