using UnityEngine;
using UnityEngine.UI;

namespace Controllers
{
    public class PlayerHealthController : MonoBehaviour
    {
        public static PlayerHealthController phcInstance;
        public Slider healthSlider;

        private void Awake()
        {
            phcInstance = this;
        }

        public float currentHealth, maxHealth;
        void Start()
        {
            currentHealth = maxHealth;

            healthSlider.maxValue = maxHealth;
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
        }
    }
}
