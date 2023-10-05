using UI;
using UnityEngine;

namespace Controllers
{
    public class UpgradePanelController : MonoBehaviour
    {
        public static UpgradePanelController contUpgrades;
        public SkipLevelButton skipLevelButton;
        public bool isOpen;
        public UpgradePanel[] upgradePanels;

        private void Awake()
        {
            contUpgrades = this;
        }
    

        private void Start()
        {
            gameObject.SetActive(isOpen);
            skipLevelButton.gameObject.SetActive(isOpen);
        }
    }
}
