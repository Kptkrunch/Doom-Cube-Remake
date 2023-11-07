using TechSkills;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Controllers
{
    public class TechUpgradePanel : MonoBehaviour
    {
        public Image techIcon;
        public TMP_Text techName, description, meat, metal, mineral, plastic, energy;
    
        private Tech _assignedTech;
    
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
            {
                _assignedTech.UpdateTech();
            }
            else
            {
                TechController.ContTechCon.AddTechToList(_assignedTech);
            }

            gameObject.SetActive(false);
        }
    }
}
