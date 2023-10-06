using System;
using UnityEngine;

namespace Controllers
{
    public class PlayerController : MonoBehaviour
    {
        public static PlayerController contPlayer;
        public float moveSpeed;
        public Animator animator;
        public SpriteRenderer spriteRenderer;
        public GameObject weapons;
        public GameObject deathRays;
        public Rigidbody2D rb2d;
        private static readonly int IsMoving = Animator.StringToHash("IsMoving");

        private void Awake()
        {
            contPlayer = this;
        }

        private void OnDisable()
        {
            UIController.contUI.gameOver.gameObject.SetActive(true);
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
                // spriteRenderer.flipX = true;
                deathRays.transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
                weapons.transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
            }
            else if (moveInput.x > 0)
            {
                // spriteRenderer.flipX = false;
                deathRays.transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
                weapons.transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);

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
