using System.Collections.Generic;
using Controllers;
using MoreMountains.Tools;
using UnityEngine;
using UnityEngine.Serialization;

public class MusicGroupController : MonoBehaviour
{
    public static MusicGroupController Instance;
    public List<MusicController> musicControllers = new();
    public float musicVolume = 1f, sfxVolume = 1f, uiVolume = 1f;

    public MusicController musicA, musicB, musicC;
    private void Awake()
    {
        Instance = this;
        PopulateMusicList();
    }

    private void Start()
    {
        // 0 is for title screen music, 1 is for level 1 music, and 2 is for game over music
        Instance.musicControllers[0].player.FeedbacksList[1].Play(transform.position);
        AdjustMusicVolume(musicVolume);
    }


    private void PopulateMusicList()
    {
        musicControllers.Add(musicA);
        musicControllers.Add(musicB);
        musicControllers.Add(musicC);
    }
    
    private void AdjustMusicVolume(float newVolume)
    {
        MMSoundManager.Instance.SetTrackVolume(MMSoundManager.MMSoundManagerTracks.Music, newVolume);
    }
}