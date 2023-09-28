using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public static LevelController lvlController;
    public List<int> expLevels;
    public int currentLevel = 1, maxLevel = 100;

    private void Awake()
    {
        lvlController = this;
    }

    private void Start()
    {
        while (expLevels.Count < maxLevel)
        {
            expLevels.Add(Mathf.CeilToInt(expLevels[expLevels.Count - 1] * 1.1f));
        }
    }

    public void LevelUp()
    {
        currentLevel++;
        GeneralTextController.generalTextControllerController.ShowText("Level Up", PlayerController.pController.transform.position);
    }
}
