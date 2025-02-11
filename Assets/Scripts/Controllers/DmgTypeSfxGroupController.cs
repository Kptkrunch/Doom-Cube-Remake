using System.Collections.Generic;
using UnityEngine;

public class DmgTypeSfxGroupController : MonoBehaviour
{
    public static DmgTypeSfxGroupController Instance;
    public List<DmgTypeSfxController> sfxControllers = new();
    public DmgTypeSfxController 
        deathray, 
        acid, 
        fire, 
        energy,
        mind, 
        solid, 
        slice, 
        stab;
    
    private void Awake()
    {
        Instance = this;
        PopulateSfxList();
    }


    private void PopulateSfxList()
    {
        sfxControllers.Add(deathray);
        sfxControllers.Add(acid);
        sfxControllers.Add(fire);
        sfxControllers.Add(energy);
        sfxControllers.Add(mind);
        sfxControllers.Add(solid);
        sfxControllers.Add(slice);
        sfxControllers.Add(stab);
    }
}
