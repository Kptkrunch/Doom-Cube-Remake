using UnityEngine;

public class LevelStart : MonoBehaviour
{
    public MusicManager audioPlayer;
    public int musicIndex = 0;
    private void Start()
    {
        audioPlayer.musicPlayer.FeedbacksList[musicIndex].Play(transform.position);
    }
}
