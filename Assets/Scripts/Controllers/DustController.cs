using JetBrains.Annotations;
using MoreMountains.Tools;
using UnityEngine;

namespace Controllers
{
    public class DustController : PoolController
    {
        [CanBeNull] [SerializeField] public MMSimpleObjectPooler sDust, mDust, bDust;
    }
}