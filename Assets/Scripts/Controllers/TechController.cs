using System.Collections.Generic;
using TechSkills;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Controllers
{
    public class TechController : MonoBehaviour
    {
        public int maxTech = 3;
        public static TechController contTechCon;
        public List<int> IDList;
        public List<Tech> purchasedTechList, upgradeableTech, allAvailableTechList, fullyUpgradedTech;
        private Gamepad _gamepad;
        

        private void Awake()
        {
            contTechCon = this;
        }

        private void Update()
        {
            _gamepad = Gamepad.current;
            if (_gamepad == null) return;
            CheckInput();
        }
    
        private void CheckInput()
        {
            var length = purchasedTechList.Count;
            if (_gamepad.aButton.wasPressedThisFrame && length >= 1)
            {
                purchasedTechList[0].ActivateTech("a");
            } else if (_gamepad.bButton.wasPressedThisFrame && length >= 2)
            {
                purchasedTechList[1].ActivateTech("b");
            } else if (_gamepad.xButton.wasPressedThisFrame && length >= 3)
            {
                purchasedTechList[2].ActivateTech("x");
            } else if (_gamepad.yButton.wasPressedThisFrame && length >= 4)
            {
                purchasedTechList[3].ActivateTech("y");
            }
        }
    
        public bool BrokeCheck(int meat, int metal, int mineral, int plastic, int energy)
        {
            var canAfford = true;
            var resCont = ResourceController.contRes;
            if (resCont.meat - meat < 0) canAfford = false;
            if (resCont.metal - metal < 0) canAfford = false; 
            if (resCont.mineral - mineral < 0) canAfford = false; 
            if (resCont.plastic - plastic < 0) canAfford = false;
            if (resCont.energy - energy < 0) canAfford = false;

            if (canAfford)
            {
                ResourceController.contRes.SpendResource(meat, metal, mineral, plastic, energy);
            }
            return canAfford;
        }

        public void AddTechToList(Tech tech)
        {
            if (contTechCon.purchasedTechList.Count < 4)
            {
                contTechCon.purchasedTechList.Add(tech); 
            }

            for (var i = 0; i < IDList.Count; i++) 
            {
                if (IDList[i] == 999)
                {
                    IDList[i] = tech.id;
                    return;
                }
            }
        }
    }
}