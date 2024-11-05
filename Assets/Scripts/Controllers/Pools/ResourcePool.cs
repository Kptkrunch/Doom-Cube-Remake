using MoreMountains.Tools;
using UnityEngine;

namespace Controllers.Pools
{
    public class ResourcePool : MonoBehaviour
    {
        public static ResourcePool poolRes;
        public MMSimpleObjectPooler poolMeat, poolMetal, poolMineral, poolPlastic, poolEnergy, poolExp;

        private void Awake()
        {
            poolRes = this;
        }
    }
}