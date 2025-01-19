using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using MoreMountains.Tools;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    // The numbers in the feedback list correspond to the index of the sound in the MMSoundManager
    // each index is reserved for a type of sound, with filler sounds sharing sounds with the 0 index
    // 0 = weapon muzzle, 1 = hit sound, 2 = explosion sound, 4 = passive sound
    public string musicName;
    public int id;
    public MMF_Player player;
    private List<MMF_Feedback> _musicFeedbacksList = new();

    private void Awake()
    {
        _musicFeedbacksList = player.FeedbacksList;
    }

    public void PlayMusicA()
    {
        _musicFeedbacksList[0].Play(transform.position);
    }

    public void PlayMusicB()
    {
        _musicFeedbacksList[1].Play(transform.position);
    }

    public void PlayMusicC()
    {
        _musicFeedbacksList[2].Play(transform.position);
    }

    private void PlaySound(MMSoundManagerSound sound)
    {
        if (sound.Source != null)
        {
            sound.Source.Play();
        }
    }
}
