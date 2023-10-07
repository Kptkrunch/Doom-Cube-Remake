using MoreMountains.Tools;
using UnityEngine;

public class BombPool : MonoBehaviour
{
    public static BombPool poolBomb;
    public MMSimpleObjectPooler paraBombPool, paraExpPool;
    
    
    private void Awake()
    {
        poolBomb = this;
    }
}
