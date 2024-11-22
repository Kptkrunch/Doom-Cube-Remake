using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using UnityEngine;

public class SceneManagerPlayers : MonoBehaviour
{
    public static SceneManagerPlayers Instance;
    
    private void Awake()
    {
        Instance = this;
    }
}
