using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceController : MonoBehaviour
{
    public static ExperienceController expController;
    public ItemDropper expDrop;
    public LevelController expLevelController;

    private void Awake()
    {
        expController = this;
    }

    public int currentExp;

    public void GetExp(int exp)
    {
        currentExp += exp;
        if (currentExp >= expLevelController.expLevels[expLevelController.currentLevel])
        {
            expLevelController.LevelUp();
            if (exp > 0)
            {
                currentExp = 0;
            }
        }
    }
}
