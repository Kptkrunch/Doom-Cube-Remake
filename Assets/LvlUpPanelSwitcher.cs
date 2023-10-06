using UnityEngine;

public class LvlUpPanelSwitcher : MonoBehaviour
{
    public static LvlUpPanelSwitcher contLvlPanSwitch;

    private void Awake()
    {
        contLvlPanSwitch = this;
    }
}
