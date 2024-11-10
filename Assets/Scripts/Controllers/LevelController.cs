using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using TechSkills;
using UI;
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
            while (expLevels.Count < maxLevel) expLevels.Add(Mathf.CeilToInt(expLevels[expLevels.Count - 1] * 1.1f));
        }

        public void LevelUp()
        {
            ExperienceController.contExp.currentExp = 0;
            StartCoroutine(ShowCongrats());
            currentLevel++;
            ExpBar.expBar.UpdateExpBar(
                ExperienceController.contExp.currentExp,
                contExpLvls.currentLevel,
                contExpLvls.expLevels[currentLevel]);

            UpgradePanelController.contUpgrades.gameObject.SetActive(true);
            UpgradePanelController.contUpgrades.skipLevelButton.gameObject.SetActive(true);
            TechPanelController.ContTechPanel.gameObject.SetActive(true);
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

        protected virtual void WeaponsLevelUpProcess()
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
                availableWeapons.AddRange(WeaponController.contWeps.allWeapons);

            for (var i = WeaponController.contWeps.upgradableWeapons.Count; i < 3; i++)
                switch (availableWeapons.Count)
                {
                    case > 0:
                    {
                        var selectedWeapon = Random.Range(0, availableWeapons.Count);
                        WeaponController.contWeps.upgradableWeapons.Add(availableWeapons[selectedWeapon]);
                        availableWeapons.RemoveAt(selectedWeapon);
                        break;
                    }
                }

            for (var i = 0; i < UpgradePanelController.contUpgrades.upgradePanels.Length; i++)
                UpgradePanelController.contUpgrades.upgradePanels[i]
                    .UpdatePanelDisplay(WeaponController.contWeps.upgradableWeapons[i]);

            for (var i = 0; i < UpgradePanelController.contUpgrades.upgradePanels.Length; i++)
                UpgradePanelController.contUpgrades.upgradePanels[i].gameObject
                    .SetActive(i < WeaponController.contWeps.upgradableWeapons.Count);
        }

        private void TechLevelUpProcess()
        {
            var availableTech = new List<Tech>();
            availableTech.AddRange(TechController.ContTechCon.purchasedTechList);

            if (availableTech.Count > 0)
            {
                var selectedTech = Random.Range(0, availableTech.Count);
                TechController.ContTechCon.upgradeableTech.Add(availableTech[selectedTech]);
                availableTech.RemoveAt(selectedTech);
            }

            if (TechController.ContTechCon.purchasedTechList.Count <
                TechController.ContTechCon.maxTech + TechController.ContTechCon.fullyUpgradedTech.Count &&
                TechController.ContTechCon.allAvailableTechList.Count != 0)
                availableTech.AddRange(TechController.ContTechCon.allAvailableTechList);

            for (var i = TechController.ContTechCon.upgradeableTech.Count; i < 3; i++)
                if (availableTech.Count > 0)
                {
                    var selectedTech = Random.Range(0, availableTech.Count);
                    TechController.ContTechCon.upgradeableTech.Add(availableTech[selectedTech]);
                    availableTech.RemoveAt(selectedTech);
                }

            for (var i = 0; i < TechPanelController.ContTechPanel.techUpgradePanels.Length; i++)
                TechPanelController.ContTechPanel.techUpgradePanels[i]
                    .UpdatePanelDisplay(TechController.ContTechCon.upgradeableTech[i]);

            for (var i = 0; i < TechPanelController.ContTechPanel.techUpgradePanels.Length; i++)
                TechPanelController.ContTechPanel.techUpgradePanels[i].gameObject
                    .SetActive(i < TechController.ContTechCon.upgradeableTech.Count);
        }
    }
}