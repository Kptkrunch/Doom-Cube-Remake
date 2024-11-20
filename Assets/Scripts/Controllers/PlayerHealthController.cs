using MoreMountains.Feedbacks;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers
{
    public class PlayerHealthController : MonoBehaviour
    {
        public static PlayerHealthController contPHealth;
        public Slider healthSlider;
        public float currentHealth;
        public float lastAttackedTime;
        public GameObject enemyAttack;

        private float _maxHealth;


        private void Awake()
        {
            contPHealth = this;
        }

        private void Start()
        {
            _maxHealth = PlayerStatsController.contStats.maxHealth;
            currentHealth = _maxHealth;

            healthSlider.maxValue = _maxHealth;
            healthSlider.value = currentHealth;
        }

        public void TakeDamage(float damage)
        {
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                gameObject.SetActive(false);
                UIController.contUI.gameOver.SetActive(true);
            }
            ShowDamage(damage, 1f);
            healthSlider.value = currentHealth;
            lastAttackedTime = Time.time;
        }

        private void ShowDamage(float theDamage, float intensity = 1f)
        {
            var floatingText = PlayerDamageNumberController.ContPlayerDmgNum
                .player.GetFeedbackOfType<MMF_FloatingText>();
            floatingText.Value = theDamage.ToString();
            PlayerDamageNumberController.ContPlayerDmgNum.player.PlayFeedbacks(transform.position);
        }
    }
}