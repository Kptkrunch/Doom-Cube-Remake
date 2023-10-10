using System;
using Controllers.Pools;
using UnityEngine;

namespace TechSkills
{
    public class Tech : MonoBehaviour
    {
        public int id;
        public int techLevel;
        public string description;
        public Sprite techImage;
        public int costMeat, costMetal, costMineral, costPlastic, costEnergy;
        public float cooldown; 
        private float _cooldownTimer;

        public virtual void ActivateTech(string button, int techId)
        {
            if (Time.time > _cooldownTimer)
            {
                var activator = TechActivator.techActivator;
                switch (button)
                {
                    case "a":
                        activator.techId = techId;
                        activator.gameObject.SetActive(true);
                        break;
                    case "x":
                        activator.techId = techId;
                        activator.gameObject.SetActive(true);
                        break;
                    case "y":
                        activator.techId = techId;
                        activator.gameObject.SetActive(true);
                        break;
                    case "b":
                        activator.techId = techId;
                        activator.gameObject.SetActive(true);
                        break;
                }

                _cooldownTimer = Time.time + cooldown;
            }
            else
            {
                Debug.Log("Tech on cooldown");
            }
        }
        
        public void UpdateTech()
        {
        
        }
    }
}

