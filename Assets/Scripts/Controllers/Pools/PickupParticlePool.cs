using MoreMountains.Tools;
using UnityEngine;

namespace Controllers.Pools
{
    public class PickupParticlePool : MonoBehaviour
    {
        public static PickupParticlePool poolPickup;
        public MMSimpleObjectPooler poolDark, poolRed;

        private void Awake()
        {
            poolPickup = this;
        }
    }
}