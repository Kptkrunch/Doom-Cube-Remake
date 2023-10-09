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
                upgradeDescription.text = theWeapon.stats[theWeapon.weaponLevel].upgradeText;
                weaponImage.sprite = theWeapon.weaponIcon;
                weaponNameAndLvl.text = theWeapon.stats[theWeapon.weaponLevel].name + ": Lvl-" + theWeapon.weaponLevel;
            }
            else
            {
                upgradeDescription.text = "Unlock the " + theWeapon.stats[theWeapon.weaponLevel].name;
                weaponImage.sprite = theWeapon.weaponIcon;
                weaponNameAndLvl.text = theWeapon.stats[theWeapon.weaponLevel].name;
            }
            _assignedWeapon = theWeapon;

        }

        public void SelectUpgrade()
        {
            if (_assignedWeapon != null)
            {
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
                TechPanelController.contTechPanel.gameObject.SetActive(false);
                Time.timeScale = 1f;
            }
        }
    }
}
