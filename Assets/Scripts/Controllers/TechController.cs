using System;
using System.Collections.Generic;
using Controllers;
using TechSkills;
using UnityEngine;
using UnityEngine.InputSystem;

public class TechController : MonoBehaviour
{
    public static TechController contTechCon;
    public List<Tech> purchasedTechList, upgradeableTech, allAvailableTechList, fullyUpgradedTech;
    private Gamepad _gamepad;

    private void Awake()
    {
        contTechCon = this;
    }

    private void Update()
    {
        _gamepad = Gamepad.current;
        if (_gamepad == null) return;
        CheckInput();
    }
    
    private void CheckInput()
    {
        if (_gamepad.aButton.wasPressedThisFrame)
        {
            purchasedTechList[0].ActivateTech("a");
        }

        if (_gamepad.bButton.wasPressedThisFrame)
        {
            purchasedTechList[1].ActivateTech("x");
        }

        if (_gamepad.xButton.wasPressedThisFrame)
        {
            purchasedTechList[2].ActivateTech("y");
        }

        if (_gamepad.yButton.wasPressedThisFrame)
        {
            purchasedTechList[3].ActivateTech("b");
        }
    }
    
    public bool BrokeCheck(int meat, int metal, int mineral, int plastic, int energy)
    {
        var canAfford = true;
        var resCont = ResourceController.contRes;
        if (resCont.meat - meat < 0) canAfford = false;
        if (resCont.metal - metal < 0) canAfford = false; 
        if (resCont.mineral - mineral < 0) canAfford = false; 
        if (resCont.plastic - plastic < 0) canAfford = false;
        if (resCont.energy - energy < 0) canAfford = false;

        if (canAfford)
        {
            ResourceController.contRes.SpendResource(meat, metal, mineral, plastic, energy);
        }
        return canAfford;
    }

    public void AddTechToList(Tech tech)
    {
        if (contTechCon.purchasedTechList.Count < 4)
        {
            contTechCon.purchasedTechList.Add(tech); 
        }
    }

    public void RemoveTechFromList(int techIndex)
    {
        if (contTechCon.purchasedTechList[techIndex])
        {
            contTechCon.purchasedTechList.RemoveAt(techIndex);
        }
    }
}
