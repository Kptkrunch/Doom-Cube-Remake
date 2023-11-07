using Controllers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Weapons;

namespace UI
{
    public class UpgradePanel : MonoBehaviour
    {
        public Image weaponImage;
        public TextMeshProUGUI weaponNameAndLvl, upgradeDescription;

        private Weapon _assignedWeapon;
    
        public void UpdatePanelDisplay(Weapon theWeapon)
        {
            if (theWeapon.gameObject.activeSelf)
            {
                upgradeDescription.text = theWeapon.stats.weaponLvls[theWeapon.stats.lvl].upgradeText;
                weaponImage.sprite = theWeapon.uiSprite;
                weaponNameAndLvl.text = theWeapon.name + ": Lvl-" + theWeapon.stats.lvl;
            }
            else
            {
                upgradeDescription.text = "Unlock the " + theWeapon.name;
                weaponImage.sprite = theWeapon.uiSprite;
                weaponNameAndLvl.text = theWeapon.name;
            }
            _assignedWeapon = theWeapon;

        }

        public void SelectUpgrade()
        {
            if (_assignedWeapon == null) return;
            if (_assignedWeapon.gameObject.activeSelf)
            {
                _assignedWeapon.UpdateWeapon();
            }
            else
            {
                WeaponController.contWeps.AddWeapon(_assignedWeapon);
            }
            UpgradePanelController.contUpgrades.gameObject.SetActive(false);
            UpgradePanelController.contUpgrades.skipLevelButton.gameObject.SetActive(false);
            TechPanelController.ContTechPanel.gameObject.SetActive(false);
            Time.timeScale = 1f;
        }
    }
}
