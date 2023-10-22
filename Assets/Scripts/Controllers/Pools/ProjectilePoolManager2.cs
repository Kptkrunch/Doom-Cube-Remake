using System.Collections.Generic;
using MoreMountains.Tools;
using UnityEngine;

namespace Controllers.Pools
{
    public class ProjectilePoolManager2 : MonoBehaviour
    {
        public static ProjectilePoolManager2 poolProj;
        public List<MMSimpleObjectPooler> projPools;
    
        private void Awake()
        {
            poolProj = this;
        }
    }
}
