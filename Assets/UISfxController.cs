using System.Collections.Generic;
using MoreMountains.Feedbacks;
using MoreMountains.Tools;
using UnityEngine;

public class UISfxController : MonoBehaviour
{
    public string uiElemName;
    public int id;
    public MMF_Player player;
    private List<MMF_Feedback> _sfxFeedbacksList = new();

    private void Awake()
    {
        _sfxFeedbacksList = player.FeedbacksList;
    }
    
    public void PlaySound()
    {
        _sfxFeedbacksList[0].Play(transform.position);
    }

    private void PlaySound(MMSoundManagerSound sound)
    {
        if (sound.Source != null)
        {
            sound.Source.Play();
        }
    }
}
