using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using UnityEngine;

public class DeathRaySfxController : MonoBehaviour
{
    public string deathRayName;
    public int id;
    public MMF_Player player;
    private List<MMF_Feedback> _sfxFeedbacksList = new();

    private void Awake()
    {
        _sfxFeedbacksList = player.FeedbacksList;
    }
        
    public void PlayDeathraySound()
    {
        var index = Random.Range(0, _sfxFeedbacksList.Count);
        _sfxFeedbacksList[index].Play(transform.position);
    }
}
