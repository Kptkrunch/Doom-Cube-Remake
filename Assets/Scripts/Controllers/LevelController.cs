using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using TechSkills;
using UnityEngine;
using Weapons;
using Random = UnityEngine.Random;

namespace Controllers
{
    public class LevelController : MonoBehaviour
    {
        public static LevelController contExpLvls;
        public List<int> expLevels;
        public int currentLevel = 1, maxLevel = 100;
        public MMF_Player player;
        public GameObject lvlUpParticle;
        private bool _closePanel;

        private void Awake()
        {
            contExpLvls = this;
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
            StartCoroutine(ShowCongrats());
            UpgradePanelController.contUpgrades.gameObject.SetActive(true);
            UpgradePanelController.contUpgrades.skipLevelButton.gameObject.SetActive(true);
            TechPanelController.contTechPanel.gameObject.SetActive(true);
            WeaponController.contWeps.upgradableWeapons.Clear();
            Time.timeScale = 0f;

            WeaponsLevelUpProcess();
            TechLevelUpProcess();
            
            StopCoroutine(ShowCongrats());
        }

        private IEnumerator ShowCongrats()
        {
            if (lvlUpParticle) lvlUpParticle.SetActive(true);
            player.PlayFeedbacks(PlayerController.contPlayer.gameObject.transform.position);
            yield return new WaitForSeconds(1f);
        }

        private void WeaponsLevelUpProcess()
        {

            var availableWeapons = new List<Weapon>();
            availableWeapons.AddRange(WeaponController.contWeps.equippedWeapons);
            if (availableWeapons.Count > 0)
            {
                var selectedWeapon = Random.Range(0, availableWeapons.Count);
                WeaponController.contWeps.upgradableWeapons.Add(availableWeapons[selectedWeapon]);
                availableWeapons.RemoveAt(selectedWeapon);
            }

            if (WeaponController.contWeps.equippedWeapons.Count <
                PlayerStatsController.contStats.maxWeapons + WeaponController.contWeps.fullyUpgradedWeapons.Count &&
                WeaponController.contWeps.allWeapons.Count != 0)
            {
                availableWeapons.AddRange(WeaponController.contWeps.allWeapons);
            }

            for (var i = WeaponController.contWeps.upgradableWeapons.Count; i < 3; i++)
            {
                if (availableWeapons.Count > 0)
                {
                    var selectedWeapon = Random.Range(0, availableWeapons.Count);
                    WeaponController.contWeps.upgradableWeapons.Add(availableWeapons[selectedWeapon]);
                    availableWeapons.RemoveAt(selectedWeapon);
                }
            }

            for (var i = 0; i < WeaponController.contWeps.upgradableWeapons.Count; i++)
            {
                UpgradePanelController.contUpgrades.upgradePanels[i]
                    .UpdatePanelDisplay(WeaponController.contWeps.upgradableWeapons[i]);
            }

            for (var i = 0; i < UpgradePanelController.contUpgrades.upgradePanels.Length; i++)
            {
                if (i < WeaponController.contWeps.upgradableWeapons.Count)
                {
                    UpgradePanelController.contUpgrades.upgradePanels[i].gameObject.SetActive(true);
                }
                else
                {
                    UpgradePanelController.contUpgrades.upgradePanels[i].gameObject.SetActive(false);
                }
            }
        }

        private void TechLevelUpProcess()
        {

            var availableTech = new List<Tech>();
            if (availableTech == null) throw new ArgumentNullException(nameof(availableTech));

            availableTech.AddRange(TechController.contTechCon.purchasedTechList);
            if (availableTech.Count > 0)
            {
                var selectedTech = Random.Range(0, availableTech.Count);
                TechController.contTechCon.upgradeableTech.Add(availableTech[selectedTech]);
                availableTech.RemoveAt(selectedTech);
            }

            if (TechController.contTechCon.purchasedTechList.Count <
                PlayerStatsController.contStats.maxWeapons + TechController.contTechCon.fullyUpgradedTech.Count &&
                TechController.contTechCon.allAvailableTechList.Count != 0)
            {
                availableTech.AddRange(TechController.contTechCon.allAvailableTechList);
            }

            for (var i = TechController.contTechCon.upgradeableTech.Count; i < 3; i++)
            {
                
                if (availableTech.Count > 0)
                {
                    var selectedTech = Random.Range(0, availableTech.Count);
                    TechController.contTechCon.upgradeableTech.Add(availableTech[selectedTech]);
                    availableTech.RemoveAt(selectedTech);
                }
            }

            for (var i = 0; i < TechController.contTechCon.upgradeableTech.Count; i++)
            {
                TechPanelController.contTechPanel.techUpgradePanels[i].UpdatePanelDisplay(TechController.contTechCon.upgradeableTech[i]);
            }

            for (var i = 0; i < TechController.contTechCon.upgradeableTech.Count; i++)
            {
                if (i < TechController.contTechCon.upgradeableTech.Count)
                {
                    TechController.contTechCon.upgradeableTech[i].gameObject.SetActive(true);
                }
                else
                {
                    TechController.contTechCon.upgradeableTech[i].gameObject.SetActive(false);
                }
            }
        }
    }
}
