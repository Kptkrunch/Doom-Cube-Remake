using System;
using TechSkills;
using TMPro;
using UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Controllers
{
    public class TechUpgradePanel : MonoBehaviour
    {
        public Image techIcon;
        public TMP_Text techName, description, meat, metal, mineral, plastic, energy;
        public RerollTechButton rerollButton;
        public int rerollCost;
        public bool rerolled;
        private Tech _assignedTech;
        private int _rerollCostTemp;


        private void Awake()
        {
            _rerollCostTemp = rerollCost;
            rerolled = false;
        }

        private void OnDisable()
        {
            rerolled = false;
        }

        public void UpdatePanelDisplay(Tech theTech)
        {
            description.text = theTech.description;
            techIcon.sprite = theTech.techImage;
            techName.text = theTech.name + ": Lvl-" + theTech.techLevel;
            meat.text = theTech.costMeat.ToString();
            metal.text = theTech.costMetal.ToString();
            mineral.text = theTech.costMineral.ToString();
            plastic.text = theTech.costPlastic.ToString();
            energy.text = theTech.costEnergy.ToString();

            _assignedTech = theTech;
        }

        public void SelectUpgrade()
        {
            if (!_assignedTech) return;

            if (!TechController.ContTechCon.BrokeCheck(
                    _assignedTech.costMeat,
                    _assignedTech.costMetal,
                    _assignedTech.costMineral,
                    _assignedTech.costPlastic,
                    _assignedTech.costEnergy)) return;
            if (!_assignedTech.gameObject.activeSelf)
                _assignedTech.ResetOrInitTechObject();
            else
                TechController.ContTechCon.AddTechToList(_assignedTech);
            EventSystem.current.SetSelectedGameObject(UpgradePanelController.contUpgrades.defaultUISelectionButton, null);

            gameObject.SetActive(false);
        }
        
        public void RerollThisSlot()
        {
            if (rerolled) return;
            rerolled = true;
            ResourceController.contRes.energy -= _rerollCostTemp;
            rerollCost += _rerollCostTemp;
            rerollButton.costText.text = rerollCost.ToString();
            
            var techIndex = Random.Range(0, TechController.ContTechCon.allAvailableTechList.Count - 1);
            var newTech = TechController.ContTechCon.allAvailableTechList[techIndex];
            _assignedTech = newTech;
            UpdatePanelDisplay(newTech);
        }
    }
}