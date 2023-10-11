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

        private void Start()
        {
            _cooldownTimer = 0;
        }

        public virtual void ActivateTech(string button, int techId)
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
        }
        
        public void UpdateTech()
        {
        
        }
    }
}

