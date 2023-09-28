using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public List<int> expLevels;
    public int currentLevel = 1, maxLevel = 100;

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
