using System.Collections.Generic;
using UnityEngine;

public class UISfxGroupController : MonoBehaviour
{
    public static UISfxGroupController Instance;
    public List<UISfxController> sfxControllers = new();
    
    public UISfxController startButton, optionsButton, exitButton, backButton;
    private void Awake()
    {
        Instance = this;
        PopulateSfxList();
    }
    
    
    private void PopulateSfxList()
    {
        sfxControllers.Add(startButton);
        sfxControllers.Add(optionsButton);
        sfxControllers.Add(exitButton);
        sfxControllers.Add(backButton);
    }
}


