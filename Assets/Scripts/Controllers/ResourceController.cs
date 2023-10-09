using TMPro;
using UnityEngine;

namespace Controllers
{
    public class ResourceController : MonoBehaviour
    {
        public static ResourceController contRes;
        public int meat, metal, mineral, plastic, energy;
        public TMP_Text meatText, metalText, mineralText, plasticText, energyText;
        
        private void Awake()
        {
            contRes = this;
        }

        public void UpdateResources()
        {
            meatText.text = meat.ToString();
            metalText.text = metal.ToString();
            mineralText.text = mineral.ToString();
            plasticText.text = plastic.ToString();
            energyText.text = energy.ToString();
        }

        public void SpendResource(int meatSpent, int metalSpent, int mineralSpent, int plasticSpent, int enerygySpent)
        {
            meat -= meatSpent;
            metal -= metalSpent;
            mineral -= mineralSpent;
            plastic -= plasticSpent;
            energy -= enerygySpent;
            UpdateResources();
        }
    }
}