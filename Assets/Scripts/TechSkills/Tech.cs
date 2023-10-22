using Controllers;
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
        protected float cooldownTimer;
        private TechController _techCont;

        private void Start()
        {
            cooldownTimer = 0;
            _techCont = TechController.contTechCon;
        }

        // ReSharper disable Unity.PerformanceAnalysis
        public void ActivateTech(string button)
        {
            var tech = TechController.contTechCon.purchasedTechList;
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
        
        public void UpdateTech()
        {
        
        }
    }
}

