using Controllers;
using GenUtilsAndTools;
using MoreMountains.Feedbacks;
using UnityEngine;
using UnityEngine.Serialization;
using Weapons.Projectiles;

namespace TechSkills
{
    public class TurretBottomSectionTech : Tech
    {
        private static readonly int GreyscaleBlend = Shader.PropertyToID("_GreyscaleBlend");
        public float currentHealth, maxHealth;

        public TurretTopSectionTech turretTop;
        public Rigidbody2D rb2d;
        
        private float _percentDamaged;
        
        public override void TakeDamage(float damage)
        {
            currentHealth -= damage;
            _percentDamaged = 1 - (currentHealth / maxHealth);
            turretTop.percentDamaged = _percentDamaged;
            
            ShowDamage(damage, 1f);
            UpdateShaderGreyscaleBlend();
            turretTop.UpdateShaderGreyscaleBlend();
            if (currentHealth <= 0)
            {
                gameObject.SetActive(false);
                // get destruction particle
            }
        }

        private void ShowDamage(float damage, float intensity = 1f)
        {
            var floatingText = TechDamageNumberController.ContTechDmgNum.player.GetFeedbackOfType<MMF_FloatingText>();
            var damageString = damage.ToString();
            floatingText.Value = damageString;

            if (rb2d)
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

        public override void ResetOrInitTechObject()
        {
            currentHealth = maxHealth;
            _percentDamaged = 0f;
            UpdateShaderGreyscaleBlend();
            if (turretTop) turretTop.UpdateShaderGreyscaleBlend();
        }
    }
}