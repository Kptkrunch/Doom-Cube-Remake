using System;
using Controllers;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public static UIController contUI;
    public LevelController lvlCont;
    public GameObject healthBar, destructionBar, gameOver, upgPanel, skipButton;
    private void Awake()
    {
        contUI = this;
    }
}
