using Controllers;
using UnityEngine;

namespace UI
{
    public class SkipLevelButton : MonoBehaviour
    {
        public void SkipLevel()
        {
            UpgradePanelController.contUpgrades.gameObject.SetActive(false);
            TechPanelController.ContTechPanel.gameObject.SetActive(false);
            Time.timeScale = 1f;
            gameObject.SetActive(false);
        }
    }
}