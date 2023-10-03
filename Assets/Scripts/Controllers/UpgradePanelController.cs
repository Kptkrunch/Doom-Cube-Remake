using UI;
using UnityEngine;

namespace Controllers
{
    public class UpgradePanelController : MonoBehaviour
    {
        public static UpgradePanelController upgradePanelController;
        public SkipLevelButton skipLevelButton;
        public bool isOpen;
        public UpgradePanel[] upgradePanels;

        private void Awake()
        {
            upgradePanelController = this;
        }
    

        private void Start()
        {
            gameObject.SetActive(isOpen);
            skipLevelButton.gameObject.SetActive(isOpen);
        }
    }
}
