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

        public void SpendResource(int amount, string resType)
        {
            switch (resType)
            {
                case "meat":
                    meat -= amount;
                    break;
                case "metal":
                    metal -= amount;
                    break;
                case "mineral":
                    mineral -= amount;
                    break;
                case "plastic":
                    plastic -= amount;
                    break;
                case "energy":
                    energy -= amount;
                    break;
            }
            UpdateResources();
        }
    }
}