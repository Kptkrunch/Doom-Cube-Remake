using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathRaySfxGroupController : MonoBehaviour
{
    public static DeathRaySfxGroupController Instance;
    public List<DeathRaySfxController> sfxControllers = new();
    public DeathRaySfxController doomRay, deathBlade;
    
    private void Awake()
    {
        Instance = this;
        PopulateSfxList();
    }


    private void PopulateSfxList()
    {
        sfxControllers.Add(doomRay);
        sfxControllers.Add(deathBlade);
    }
}
