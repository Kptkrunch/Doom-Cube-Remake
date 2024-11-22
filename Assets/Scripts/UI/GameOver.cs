using Controllers;
using MoreMountains.Feedbacks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class GameOver : MonoBehaviour
    {
        public Button continueButton, goToTileButton;
        
        public void Continue()
        {
            SceneManager.LoadScene("Level1");
            Time.timeScale = 1;
            gameObject.SetActive(false);
        }

        public void GoToTitle()
        {
            SceneManager.LoadScene("TitleScreen");
            Time.timeScale = 1;
            gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            MusicManager.Instance.musicPlayer.StopFeedbacks(true);
            MusicManager.Instance.musicPlayer.FeedbacksList[4].Play(transform.position);
            Time.timeScale = 0;
            UIController.contUI.lvlCont.gameObject.SetActive(false);
            UIController.contUI.healthBar.SetActive(false);
            UIController.contUI.destructionBar.SetActive(false);
        }
    }
}