using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using MoreMountains.Tools;
using UnityEngine;

public class EnemySfxController : MonoBehaviour
{
        
    public string enemyName;
    public int id;
    public MMF_Player player;
    private List<MMF_Feedback> _sfxFeedbacksList = new();

    private void Awake()
    {
        _sfxFeedbacksList = player.FeedbacksList;
    }
        
    public void PlayHitSound()
    {
        _sfxFeedbacksList[0].Play(transform.position);
    }

    public void PlayDyingSound(string damageType)
    {
        _sfxFeedbacksList[1].Play(transform.position);
    }

    public void PlayAttackSound()
    {
        _sfxFeedbacksList[2].Play(transform.position);
    }

    public void PlayIdleSound()
    {
        _sfxFeedbacksList[3].Play(transform.position);
    }    
    
    public void PlayMovingSound()
    {
        _sfxFeedbacksList[4].Play(transform.position);
    }
}
