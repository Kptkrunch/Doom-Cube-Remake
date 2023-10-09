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
                TriggerTech(0);
        }

        if (_gamepad.bButton.wasPressedThisFrame)
        {
                TriggerTech(1);
        }

        if (_gamepad.xButton.wasPressedThisFrame)
        {
                TriggerTech(2);
        }

        if (_gamepad.yButton.wasPressedThisFrame)
        {
                TriggerTech(3);
        }
    }


    private void TriggerTech(int techIndex)
    {
        switch (techIndex)
        {
            case 0:
                purchasedTechList[0].gameObject.SetActive(true);
                break;
            case 1:
                purchasedTechList[1].gameObject.SetActive(true);
                break;
            case 2:
                purchasedTechList[2].gameObject.SetActive(true);
                break;
            case 3:
                purchasedTechList[3].gameObject.SetActive(true);
                break;
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
