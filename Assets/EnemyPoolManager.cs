using MoreMountains.Tools;
using UnityEngine;

public class EnemyPoolManager : MonoBehaviour
{
    public static EnemyPoolManager PoolEnemys;
    public MMSimpleObjectPooler muggerPool;
    
    private void Awake()
    {
        PoolEnemys = this;
    }
}
