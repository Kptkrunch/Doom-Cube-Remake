using Controllers;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class GameOver : MonoBehaviour
    {
        public Button continueButton, goToTileButton;

        private void Continue()
        {
        
        }

        private void GoToTitle()
        {
        
        }

        private void OnEnable()
        {
            UIController.contUI.lvlCont.gameObject.SetActive(false);
            UIController.contUI.healthBar.SetActive(false);
            UIController.contUI.destructionBar.SetActive(false);
        }
    }
}
