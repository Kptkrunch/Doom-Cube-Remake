using UnityEngine;
using Random = UnityEngine.Random;

namespace Weapons.WeaponModifiers
{
    public class LobAndOrBounce : MonoBehaviour
    {
        public BehaviorChecklist it;
        public Rigidbody2D rb2d;
        public int bounces;
        public float lobDistance, lobHeight, bounceTimer, bounceInterval;
        private bool _wasLobbed, _wasBounced;
        private int _initBounces;

        private void Awake()
        {
            _initBounces = bounces;
        }

        public void Bounce()
        {
            bounceTimer -= Time.deltaTime;
            switch (bounceTimer)
            {
                case <= 0:
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
                        rb2d.velocity = Vector2.zero;
                        rb2d.gravityScale = 0f;
                    }

                    break;
                }
            }
        }

        public void OnEnableLobAndOrBounce()
        {
            if (it.isLobbed && !_wasLobbed)
            {
                _wasLobbed = true;
                rb2d.velocity = new Vector2(Random.Range(-lobDistance, lobDistance), lobHeight);
            }

            if (it.doesBounce && !_wasBounced)
            {
                _wasBounced = true;
                bounceTimer = Random.Range(bounceTimer * 0.5f, bounceTimer * 1.75f);
            }
        }
    }
}