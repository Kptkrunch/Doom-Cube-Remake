using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UpgradePanel : MonoBehaviour
{
    public TextMeshProUGUI weaponNameAndLvl, upgradeDescription;
    public Image weaponImage;

    private Weapon _assignedWeapon;
    
    public void UpdatePanelDisplay(Weapon theWeapon)
    {
        if (theWeapon.gameObject.activeSelf == true)
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
            if (_assignedWeapon.gameObject.activeSelf == true)
            {
                _assignedWeapon.WeaponLevelUp();
            }
            else
            {
                WepsAndAbs.wepsAndAbs.AddWeapon(_assignedWeapon);
            }
            UpgradePanelController.upgradePanelController.gameObject.SetActive(false);
            Time.timeScale = 1f;
            _assignedWeapon.weaponLeveledUp = false;
        }
    }
}
