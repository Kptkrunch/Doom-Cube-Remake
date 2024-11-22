using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleUIController : MonoBehaviour
{
    public static TitleUIController Instance;
    public int buttonSoundId;
    public OptionsMenuController optionsMenuController;
    
    private void Awake()
    {
        Instance = this;

    }

    private void Start()
    {
        optionsMenuController.EnableTitleButtons();
        optionsMenuController.gameObject.SetActive(false);
        MusicManager.Instance.musicPlayer.FeedbacksList[0].Play(transform.position);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Level1");
        MusicManager.Instance.sfxUIButtons.FeedbacksList[buttonSoundId].Play(transform.position);
        Time.timeScale = 1;
        MusicManager.Instance.musicPlayer.FeedbacksList[0].Stop(transform.position);
    }

    public void OpenOptionsMenu()
    {
        optionsMenuController.gameObject.SetActive(true);
        MusicManager.Instance.sfxUIButtons.FeedbacksList[buttonSoundId].Play(transform.position);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
    
}
