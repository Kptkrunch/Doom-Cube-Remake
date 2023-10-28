using System.Collections.Generic;
using MoreMountains.Tools;
using UnityEngine;

namespace Controllers.Pools
{
    public class ProjectilePoolManager : MonoBehaviour
    {
        public static ProjectilePoolManager poolProj;
        public List<MMSimpleObjectPooler> projPools;
    
        private void Awake()
        {
            poolProj = this;
        }
    }
}