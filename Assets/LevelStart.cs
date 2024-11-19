using UnityEngine;

public class LevelStart : MonoBehaviour
{
    public MusicManager audioPlayer;
    public int musicIndex = 0;
    private void Awake()
    {
        audioPlayer = GameObject.Find("MusicManager").GetComponent<MusicManager>();

        if (audioPlayer == null) return;
        
        audioPlayer.musicPlayer.FeedbacksList[musicIndex].Play(transform.position);
    }
}
