using System.Collections.Generic;
using MoreMountains.Tools;
using UnityEngine;

namespace Controllers.Pools
{
    public class TechPools : MonoBehaviour
    {
        public static TechPools pools;
        public List<MMSimpleObjectPooler> techList;

        private void Awake()
        {
            pools = this;
        }
    }
}