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
            }
            healthSlider.value = currentHealth;
            lastAttackedTime = Time.time;
        }
    }
}
