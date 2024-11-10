using UnityEngine;

namespace Controllers
{
    public class PlayerController : MonoBehaviour
    {
        public static PlayerController contPlayer;
        private float _moveSpeed;
        public Animator animator;
        public GameObject weapons;
        public GameObject deathRays;
        public Rigidbody2D rb2d;
        private static readonly int IsMoving = Animator.StringToHash("IsMoving");

        private void Awake()
        {
            contPlayer = this;
        }

        private void Start()
        {
            _moveSpeed = PlayerStatsController.contStats.moveSpeed;
        }

        private void Update()
        {
            HandleMovementAndFlip();
            TurnOffLvelUpParticle();
            ShowGameOver();
        }

        private void HandleMovementAndFlip()
        {
            var moveInput = new Vector3(0f, 0f, 0f)
            {
                x = Input.GetAxisRaw("Horizontal"),
                y = Input.GetAxisRaw("Vertical")
            };

            moveInput.Normalize();
            var velocity = new Vector2(moveInput.x * _moveSpeed, moveInput.y * _moveSpeed);
            switch (moveInput.x)
            {
                case < 0:
                {
                    // spriteRenderer.flipX = true;
                    var localScale = transform.localScale;
                    deathRays.transform.localScale = new Vector3(-1, localScale.y, localScale.z);
                    weapons.transform.localScale = new Vector3(-1, localScale.y, localScale.z);
                    break;
                }
                case > 0:
                {
                    // spriteRenderer.flipX = false;
                    var localScale = transform.localScale;
                    deathRays.transform.localScale = new Vector3(1, localScale.y, localScale.z);
                    weapons.transform.localScale = new Vector3(1, localScale.y, localScale.z);
                    break;
                }
            }

            rb2d.MovePosition(rb2d.position + velocity * Time.fixedDeltaTime);
            animator.SetBool(IsMoving, moveInput != Vector3.zero);
        }

        public void ShowGameOver()
        {
            if (PlayerHealthController.contPHealth.currentHealth <= 0) UIController.contUI.gameOver.SetActive(true);
        }

        private void TurnOffLvelUpParticle()
        {
            if (!LevelController.contExpLvls.player.IsPlaying
                && LevelController.contExpLvls.lvlUpParticle.activeInHierarchy)
                LevelController.contExpLvls.lvlUpParticle.SetActive(false);
        }
    }
}