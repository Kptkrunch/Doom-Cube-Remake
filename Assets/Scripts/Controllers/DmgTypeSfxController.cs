using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using UnityEngine;

public class DmgTypeSfxController : MonoBehaviour
{
    public string damageType;
    public int id;
    public MMF_Player player;
    private List<MMF_Feedback> _sfxFeedbacksList = new();

    private void Awake()
    {
        _sfxFeedbacksList = player.FeedbacksList;
    }
        
    public void PlayDmgTypeSound()
    {
        var index = Random.Range(0, _sfxFeedbacksList.Count);
        _sfxFeedbacksList[index].Play(transform.position);
    }
}
