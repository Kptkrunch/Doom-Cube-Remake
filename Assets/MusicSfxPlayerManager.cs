using MoreMountains.Feedbacks;
using UnityEngine;
using UnityEngine.Serialization;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;
    public MMF_Player musicPlayer;
    public MMF_Player sfxUI;
    public MMF_Player sfxDeathRays;
    public MMF_Player sfxPlayerMuzzle;
    public MMF_Player sfxPlayerProjectiles;
    public MMF_Player sfxPlayerProjectiles2;
    public MMF_Player sfxPlayerHit;
    public MMF_Player sfxPlayerTech;
    public MMF_Player sfxCritter;
    public MMF_Player sfxCritter2;
    public MMF_Player sfxConstruct;
    public MMF_Player sfxConstruct2;
    public MMF_Player sfxEnemyMuzzle;
    public MMF_Player sfxEnemyProjectiles;
    public MMF_Player sfxEnemyProjectiles2;
    public MMF_Player sfxEnemyHit;
    
    
    
    private void Awake()
    {
        Instance = this;
    }
}
