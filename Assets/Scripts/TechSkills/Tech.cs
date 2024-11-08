using Controllers;
using Controllers.Pools;
using GenUtilsAndTools;
using MoreMountains.Feedbacks;
using UnityEngine;

namespace TechSkills
{
    public class Tech : MonoBehaviour
    {
        public string description, techType;

        public int id, techLevel;
        public int costMeat, costMetal, costMineral, costPlastic, costEnergy;
        
        public SpriteRenderer spriteRenderer;
        public Sprite techImage;

        // ReSharper disable Unity.PerformanceAnalysis
        public void ActivateTech(string button)
        {
            var techObj = TechPoolObjectManager.ContTechPoolObjectManager.GetTechFromPool(techType, id);
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
        
        public virtual void TakeDamage(float damage)
        {
            ShowDamage(damage);
        }

        private void ShowDamage(float damage, float intensity = 1f)
        {
            var floatingText = TechDamageNumberController.ContTechDmgNum.player.GetFeedbackOfType<MMF_FloatingText>();
            var damageString = damage.ToString();
            floatingText.Value = damageString;

            if (gameObject != null)
            {
                if (TechDamageNumberController.ContTechDmgNum != null)
                    TechDamageNumberController.ContTechDmgNum.player.PlayFeedbacks(transform.position);
            }
        }

        public virtual void ResetOrInitTechObject()
        {
            spriteRenderer.sprite = techImage;
        }
    }
}