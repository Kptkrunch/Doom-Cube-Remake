using UnityEngine;
using UnityEngine.EventSystems;

namespace Controllers
{
    public class TechPanelController : MonoBehaviour
    {
        public static TechPanelController ContTechPanel;
        public bool isOpen;
        public TechUpgradePanel[] techUpgradePanels;

        private void Awake()
        {
            ContTechPanel = this;
        }

        private void Start()
        {
            gameObject.SetActive(isOpen);
        }

        private void OnEnable()
        {
            techUpgradePanels[0].gameObject.SetActive(true);
            techUpgradePanels[1].gameObject.SetActive(true);
            techUpgradePanels[2].gameObject.SetActive(true);
        }
    }
}