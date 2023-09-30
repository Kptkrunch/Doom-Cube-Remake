using UnityEngine;

namespace Controllers
{
    public class PlayerController : MonoBehaviour
    {
        public static PlayerController pController;
        public float moveSpeed;
        public Animator animator;
        private static readonly int IsMoving = Animator.StringToHash("IsMoving");
        public SpriteRenderer spriteRenderer;

        private void Awake()
        {
            pController = this;
        }

        void Update()
        {
            Vector3 moveInput = new Vector3(0f, 0f, 0f);
            moveInput.x = Input.GetAxisRaw("Horizontal");
            moveInput.y = Input.GetAxisRaw("Vertical");
        
            moveInput.Normalize();

            if (moveInput.x < 0)
            {
                spriteRenderer.flipX = true;
            }
            else if (moveInput.x > 0)
            {
                spriteRenderer.flipX = false;
            }
        
            transform.position += moveInput * (moveSpeed * Time.deltaTime);

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
