using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsController : MonoBehaviour
{
    public static PlayerStatsController contStats;
    public float moveSpeed, maxWeapons, maxHealth;
    
    private void Awake()
    {
        contStats = this;
    }
}
