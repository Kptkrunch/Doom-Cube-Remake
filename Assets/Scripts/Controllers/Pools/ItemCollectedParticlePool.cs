using MoreMountains.Tools;
using UnityEngine;

namespace Controllers.Pools
{
    public class ItemCollectedParticlePool : MonoBehaviour
    {
        public static ItemCollectedParticlePool PoolItemCollected;
        public MMSimpleObjectPooler poolDark, poolRed;

        private void Awake()
        {
            PoolItemCollected = this;
        }
    }
}