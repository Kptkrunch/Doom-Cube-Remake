using UnityEngine;

namespace GenUtilsAndTools
{
    public class FlipX : MonoBehaviour
    {
        public Rigidbody2D rb2d;

        private void FixedUpdate()
        {
            var velocity = rb2d.velocity;
            var transform1 = transform;
            transform1.localScale = velocity.x switch
            {
                > 0 => new Vector2(-1f, 1f),
                < 0 => new Vector2(1f, 1f),
                _ => transform1.localScale
            };
        }
    }
}