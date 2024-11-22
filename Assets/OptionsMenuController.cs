using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class OptionsMenuController : MonoBehaviour
{
    public GameObject startButton, optionsButton, exitButton, backButton;
    public int buttonSoundId;
    private void OnEnable()
    {
        DisableTitleButtons();
        EventSystem.current.SetSelectedGameObject(backButton);
    }

    private void OnDisable()
    {
        EnableTitleButtons();
        EventSystem.current.SetSelectedGameObject(optionsButton);
    }

    public void EnableTitleButtons()
    {
        startButton.SetActive(true);
        optionsButton.SetActive(true);
        exitButton.SetActive(true);
    }

    public void DisableTitleButtons()
    {
        startButton.SetActive(false);
        optionsButton.SetActive(false);
        exitButton.SetActive(false);
    }

    public void CloseOptionsMenu()
    {
        MusicManager.Instance.sfxUIButtons.FeedbacksList[buttonSoundId].Play(transform.position);
        gameObject.SetActive(false);
    }
}
