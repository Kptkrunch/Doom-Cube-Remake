using System.Collections.Generic;
using MoreMountains.Tools;
using UnityEngine;

public class EnemyDeathPoolManager : MonoBehaviour
{
    public static EnemyDeathPoolManager PoolEnemyMan;

    public List<MMSimpleObjectPooler> enemyDeathPools;
    public MMSimpleObjectPooler meltingPool;
    
    private void Awake()
    {
        PoolEnemyMan = this;
    }
}