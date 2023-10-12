using JetBrains.Annotations;
using MoreMountains.Tools;
using UnityEngine;

namespace Controllers.Pools
{
    public class DustPool : PoolController
    {
        [CanBeNull] [SerializeField] public MMSimpleObjectPooler sDust, mDust, bDust;
    }
}
