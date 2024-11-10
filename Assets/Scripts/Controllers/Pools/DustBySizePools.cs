using JetBrains.Annotations;
using MoreMountains.Tools;
using UnityEngine;

namespace Controllers.Pools
{
    public class DustBySizePools : PoolController
    {
        public static DustBySizePools Instance;
        [CanBeNull] [SerializeField] public MMSimpleObjectPooler sDust, mDust, bDust;

        private void Awake()
        {
            Instance = this;
        }
    }
}