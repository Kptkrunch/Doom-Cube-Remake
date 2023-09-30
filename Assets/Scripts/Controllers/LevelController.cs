using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelController : MonoBehaviour
{
    public static LevelController lvlController;
    public List<int> expLevels;
    public int currentLevel = 1, maxLevel = 100;

    private void Awake()
    {
        lvlController = this;
    }

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
        UpgradePanelController.upgradePanelController.gameObject.SetActive(true);
        Time.timeScale = 0f;
        
        WepsAndAbs.wepsAndAbs.upgradableWeapons.Clear();

        List<Weapon> availableWeapons = new List<Weapon>();
        availableWeapons.AddRange(WepsAndAbs.wepsAndAbs.equippedWeapons);
        
        if (availableWeapons.Count > 0)
        {
            int selectedWeapon = Random.Range(0, availableWeapons.Count);
            WepsAndAbs.wepsAndAbs.upgradableWeapons.Add(availableWeapons[selectedWeapon]);
            availableWeapons.RemoveAt(selectedWeapon);
        }

        if ((WepsAndAbs.wepsAndAbs.equippedWeapons.Count < WepsAndAbs.wepsAndAbs.maxWeapons) &&
            WepsAndAbs.wepsAndAbs.allWeapons.Count != 0) ;
        {
            availableWeapons.AddRange(WepsAndAbs.wepsAndAbs.allWeapons);
        }
        for (int i = WepsAndAbs.wepsAndAbs.upgradableWeapons.Count; i < 3; i++)
        {
            if (availableWeapons.Count > 0)
            {
                int selectedWeapon = Random.Range(0, availableWeapons.Count);
                WepsAndAbs.wepsAndAbs.upgradableWeapons.Add(availableWeapons[selectedWeapon]);
                availableWeapons.RemoveAt(selectedWeapon);
            }
        }

        for (int i = 0; i < WepsAndAbs.wepsAndAbs.upgradableWeapons.Count; i++)
        {
            UpgradePanelController.upgradePanelController.upgradePanels[i].UpdatePanelDisplay(WepsAndAbs.wepsAndAbs.upgradableWeapons[i]);
        }
    }
}
