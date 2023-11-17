using System.Collections.Generic;
using MoreMountains.Tools;
using UnityEngine;

public class EnemyPoolManager : MonoBehaviour
{
    public static EnemyPoolManager PoolEnemys;
    public List<MMSimpleObjectPooler> wavePools;
    
    public MMSimpleObjectPooler muggerPool;
    public MMSimpleObjectPooler joePool;
    
    private void Awake()
    {
        PoolEnemys = this;
    }
}
