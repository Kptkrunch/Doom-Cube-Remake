using System.Collections.Generic;
using MoreMountains.Tools;
using UnityEngine;

namespace Controllers.Pools
{
    public class TechProjectilePools : MonoBehaviour
    {
        public static TechProjectilePools ProjectilePools;
        public List<MMSimpleObjectPooler> projectilePoolList = new();
        
        private void Awake()
        {
            ProjectilePools = this;
        }

        public GameObject GetProjectileById(int projectileId)
        {
            var projectile = projectilePoolList[projectileId].GetPooledGameObject();
            return projectile;
        }
    }
}
