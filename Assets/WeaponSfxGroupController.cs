using System.Collections.Generic;
using Controllers;
using UnityEngine;
using UnityEngine.Serialization;

public class WeaponSfxGroupController : MonoBehaviour
{
    public static WeaponSfxGroupController Instance;
    public List<WeaponSfxController> sfxControllers = new();

    public WeaponSfxController deltaBuster,
        meatSeekers,
        mechaMeatSeperator, 
        mineSlayer,
        mutagenicNanites,
        orbitalObliterator,
        parabalombs,
        plasmaBurster,
        roastorbs,
        seeringLance,
        type42NeuralSubsumer,
        gravulcanWelder;
    private void Awake()
    {
        Instance = this;
        PopulateSfxList();
    }


    private void PopulateSfxList()
    {
        sfxControllers.Add(deltaBuster);
        sfxControllers.Add(meatSeekers);
        sfxControllers.Add(mechaMeatSeperator);
        sfxControllers.Add(mineSlayer);
        sfxControllers.Add(mutagenicNanites);
        sfxControllers.Add(orbitalObliterator);
        sfxControllers.Add(parabalombs);
        sfxControllers.Add(plasmaBurster);
        sfxControllers.Add(roastorbs);
        sfxControllers.Add(seeringLance);
        sfxControllers.Add(type42NeuralSubsumer);
        sfxControllers.Add(gravulcanWelder);
    }
}
