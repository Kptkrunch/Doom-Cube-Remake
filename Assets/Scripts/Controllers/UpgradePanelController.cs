using UI;
using UnityEngine;

namespace Controllers
{
    public class UpgradePanelController : MonoBehaviour
    {
        public static UpgradePanelController upgradePanelController;
        public SkipLevelButton skipLevelButton;
        public bool isOpen = false;
        private void Awake()
        {
            upgradePanelController = this;
        }
    
        public UpgradePanel[] upgradePanels;

        private void Start()
        {
            gameObject.SetActive(isOpen);
            skipLevelButton.gameObject.SetActive(isOpen);
        }
    }
}
