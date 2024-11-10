using System.Collections.Generic;
using MoreMountains.Tools;
using UnityEngine;

namespace Controllers.Pools
{
    public class EnemyDeathPoolManager : MonoBehaviour
    {
        public static EnemyDeathPoolManager PoolEnemyDeathPoolManager;

        public List<MMSimpleObjectPooler> enemyDeathPools;
        public MMSimpleObjectPooler meltingPool;
        public MMSimpleObjectPooler burningPool;
        public MMSimpleObjectPooler deathAshPool;
        public MMSimpleObjectPooler energyPool;
        
        private void Awake()
        {
            PoolEnemyDeathPoolManager = this;
        }
    }
}