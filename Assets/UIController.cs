using Controllers;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public static UIController contUI;
    public LevelController lvlCont;
    public LvlUpPanelSwitcher upgCont;
    public GameOver gameOver;
    public GameObject healthBar, destructionBar;
    private void Awake()
    {
        contUI = this;
    }
}
