using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using UnityEngine;

public class ScreenFeedbackGroupController : MonoBehaviour
{
    public static ScreenFeedbackGroupController Instance;
    
    // 0 = screen shake, 1 = screen flash, 2 = screen fade
    public List<MMF_Player> screenFeedbacks;

    public MMF_Player screenShakePlayer, screenFlashPlayer, screenFadePlayer;
    private void Awake()
    {
        Instance = this;
        PopulatePlayerList();
    }

    private void PopulatePlayerList()
    {
        screenFeedbacks.Add(screenShakePlayer);
        screenFeedbacks.Add(screenFlashPlayer);
        screenFeedbacks.Add(screenFadePlayer);
    }
}
