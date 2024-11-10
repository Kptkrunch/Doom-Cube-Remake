using System.Collections.Generic;
using MoreMountains.Tools;
using UnityEngine;

namespace Controllers.Pools
{
    public class TechCritterPools : MonoBehaviour
    {
        public static TechCritterPools CritterPools;
        public List<MMSimpleObjectPooler> critterPoolList = new();

        private void Awake()
        {
            CritterPools = this;
        }

        public GameObject GetCritterById(int critterId)
        {
            var critter = critterPoolList[critterId].GetPooledGameObject();
            return critter;
        }
    }
}