using Controllers;
using Controllers.Pools;
using MoreMountains.Feedbacks;
using UnityEngine;

namespace TechSkills
{
    public class Tech : MonoBehaviour
    {
        private static readonly int GreyscaleBlend = Shader.PropertyToID("_GreyscaleBlend");
        public string description;

        public int id, techLevel;
        public int costMeat, costMetal, costMineral, costPlastic, costEnergy;

        public float currentHealth, maxHealth, cooldownTimer;
        
        [SerializeField] private TechController techCont;
        public SpriteRenderer spriteRenderer;
        public Sprite techImage;
        public Rigidbody2D techRb2d;

        private float _percentDamaged;

        private void Awake()
        {
            ResetOrInitTechObject();
        }

        private void Start()
        {
            ResetOrInitTechObject();
            techCont = TechController.ContTechCon;
            techRb2d = GetComponent<Rigidbody2D>();
        }

        // ReSharper disable Unity.PerformanceAnalysis
        public void ActivateTech(string button)
        {
            var techObj = TechPools.pools.techList[id].GetPooledGameObject();
            switch (button)
            {
                case "a":
                    techObj.SetActive(true);
                    techObj.transform.position = PlayerController.contPlayer.transform.position;
                    break;
                case "b":
                    techObj.SetActive(true);
                    techObj.transform.position = PlayerController.contPlayer.transform.position;
                    break;
                case "x":
                    techObj.SetActive(true);
                    techObj.transform.position = PlayerController.contPlayer.transform.position;
                    break;
                case "y":
                    techObj.SetActive(true);
                    techObj.transform.position = PlayerController.contPlayer.transform.position;
                    break;
            }
        }

        public void TakeDamage(float damage)
        {
            currentHealth -= damage;
            _percentDamaged = 1 - (currentHealth / maxHealth);
            
            ShowDamage(damage, 1f);
            UpdateShaderGreyscaleBlend();
            if (currentHealth <= 0)
            {
                gameObject.SetActive(false);
            }
        }

        private void ShowDamage(float damage, float intensity = 1f)
        {
            var floatingText = TechDamageNumberController.ContTechDmgNum
                .player.GetFeedbackOfType<MMF_FloatingText>();
            var damageString = damage.ToString();
            floatingText.Value = damageString;

            if (techRb2d)
            {
                if (TechDamageNumberController.ContTechDmgNum != null)
                    TechDamageNumberController.ContTechDmgNum.player.PlayFeedbacks(transform.position);
            }
        }

        private void UpdateShaderGreyscaleBlend()
        {
            if (spriteRenderer != null && _percentDamaged > 0f)
            {
                spriteRenderer.sharedMaterial.SetFloat(GreyscaleBlend, _percentDamaged);
            }
        }
        
        public void ResetOrInitTechObject()
        {
            cooldownTimer = 0;
            _percentDamaged = 0f;
            currentHealth = maxHealth;
        }
    }
}