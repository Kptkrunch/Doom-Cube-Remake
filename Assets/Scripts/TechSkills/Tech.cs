using Controllers;
using Controllers.Pools;
using MoreMountains.Feedbacks;
using UnityEngine;

namespace TechSkills
{
    public class Tech : MonoBehaviour
    {
        public int id;
        public int techLevel;
        public string description;
        public Sprite techImage;
        public float maxHealth;
        public int costMeat, costMetal, costMineral, costPlastic, costEnergy;
        protected float CooldownTimer;
        private TechController _techCont;
        [SerializeField] private float _currentHealth;
        [SerializeField] protected Rigidbody2D rb2d;

        private void Start()
        {
            CooldownTimer = 0;
            _techCont = TechController.ContTechCon;
            _currentHealth = maxHealth;
            rb2d = GetComponent<Rigidbody2D>();
        }

        // ReSharper disable Unity.PerformanceAnalysis
        public void ActivateTech(string button)
        {
            var tech = TechController.ContTechCon.purchasedTechList;
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
            _currentHealth -= damage;
            if (_currentHealth <= 0)
            {
                _currentHealth = maxHealth;
                gameObject.SetActive(false);
            }

            ShowDamage(damage, 1f);
        }

        private void ShowDamage(float theDamage, float intensity = 1f)
        {
            var floatingText = TechDamageNumberController.ContTechDmgNum.player.GetFeedbackOfType<MMF_FloatingText>();
            floatingText.Value = theDamage.ToString();
            TechDamageNumberController.ContTechDmgNum.player.PlayFeedbacks(transform.position);
        }

        public void UpdateTech()
        {
        }
    }
}