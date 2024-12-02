using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySfxGroupController : MonoBehaviour
{
    public static EnemySfxGroupController Instance;
    public List<EnemySfxController> sfxControllers = new();
    public EnemySfxController muricaJo, thug;
    
    private void Awake()
    {
        Instance = this;
        PopulateSfxList();
    }


    private void PopulateSfxList()
    {
        sfxControllers.Add(muricaJo);
        sfxControllers.Add(thug);
    }
}
