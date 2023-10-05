using UnityEngine;

namespace Controllers
{
    public class PlayerController : MonoBehaviour
    {
        public static PlayerController contPlayer;
        public float moveSpeed;
        public Animator animator;
        public SpriteRenderer spriteRenderer;
        public Rigidbody2D rb2d;

        private static readonly int IsMoving = Animator.StringToHash("IsMoving");

        private void Awake()
        {
            contPlayer = this;
        }

        private void Update()
        {
            var moveInput = new Vector3(0f, 0f, 0f)
            {
                x = Input.GetAxisRaw("Horizontal"),
                y = Input.GetAxisRaw("Vertical")
            };

            moveInput.Normalize();
            var velocity = new Vector2(moveInput.x * moveSpeed, moveInput.y * moveSpeed);
            if (moveInput.x < 0)
            {
                var transform1 = transform;
                transform1.localScale = new Vector2(-1f, transform1.localScale.y);
            }
            else if (moveInput.x > 0)
            {
                var transform1 = transform;
                transform1.localScale = new Vector2(1f, transform1.localScale.y);
            }
        
            rb2d.MovePosition(rb2d.position + velocity * Time.fixedDeltaTime);
            
            if (moveInput != Vector3.zero)
            {
                animator.SetBool(IsMoving, true);
            }
            else
            {
                animator.SetBool(IsMoving, false);
            }
        }
    }
}
