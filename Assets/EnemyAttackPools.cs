using System.Collections.Generic;
using MoreMountains.Tools;
using UnityEngine;

public class EnemyAttackPools : MonoBehaviour
{
    public static EnemyAttackPools PoolEnemyAtk;
    public List<MMSimpleObjectPooler> attackList;
    public MMSimpleObjectPooler muggerAtkPool;

    private void Awake()
    {
        PoolEnemyAtk = this;
    }
}
