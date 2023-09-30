using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ExpBar : MonoBehaviour
{
    public static ExpBar expBar;
    public Slider fillBar;
    public TMP_Text levelText;

    private float _currentValue;
    private float _maxValue;
    private void Awake()
    {
        expBar = this;
    }

    public void UpdateExpBar(float currentExp, int currentLevel, float expToLevel)
    {
        fillBar.maxValue = expToLevel;
        fillBar.value = currentExp;

        levelText.text = "Level: " + currentLevel;
    }
}
