using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
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
            WepsAndAbs.contWepsAbs.upgradableWeapons.Clear();

            var availableWeapons = new List<Weapon>();
            availableWeapons.AddRange(WepsAndAbs.contWepsAbs.equippedWeapons);
        
            if (availableWeapons.Count > 0)
            {
                var selectedWeapon = Random.Range(0, availableWeapons.Count);
                WepsAndAbs.contWepsAbs.upgradableWeapons.Add(availableWeapons[selectedWeapon]);
                availableWeapons.RemoveAt(selectedWeapon);
            }

            if (WepsAndAbs.contWepsAbs.equippedWeapons.Count <
                PlayerStatsController.contStats.maxWeapons + WepsAndAbs.contWepsAbs.fullyUpgradedWeapons.Count &&
                WepsAndAbs.contWepsAbs.allWeapons.Count != 0)
            {
                availableWeapons.AddRange(WepsAndAbs.contWepsAbs.allWeapons);
            }
            for (var i = WepsAndAbs.contWepsAbs.upgradableWeapons.Count; i < 3; i++)
            {
                if (availableWeapons.Count > 0)
                {
                    var selectedWeapon = Random.Range(0, availableWeapons.Count);
                    WepsAndAbs.contWepsAbs.upgradableWeapons.Add(availableWeapons[selectedWeapon]);
                    availableWeapons.RemoveAt(selectedWeapon);
                }
            }

            for (var i = 0; i < WepsAndAbs.contWepsAbs.upgradableWeapons.Count; i++)
            {
                UpgradePanelController.contUpgrades.upgradePanels[i].UpdatePanelDisplay(WepsAndAbs.contWepsAbs.upgradableWeapons[i]);
            }

            for (var i = 0; i < UpgradePanelController.contUpgrades.upgradePanels.Length; i++)
            {
                if (i < WepsAndAbs.contWepsAbs.upgradableWeapons.Count)
                {
                    UpgradePanelController.contUpgrades.upgradePanels[i].gameObject.SetActive(true);
                }
                else
                {
                    UpgradePanelController.contUpgrades.upgradePanels[i].gameObject.SetActive(false);
                }
            }
            StopCoroutine(ShowCongrats());
            Time.timeScale = 0f;
        }

        private IEnumerator ShowCongrats()
        {
            if (lvlUpParticle) lvlUpParticle.SetActive(true);
            player.PlayFeedbacks(PlayerController.contPlayer.gameObject.transform.position);
            yield return new WaitForSeconds(1f);
        }
    }
}
