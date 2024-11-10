using Controllers.Pools;
using UnityEngine;

namespace GenUtilsAndTools
{
    public class TechPoolObjectManager : MonoBehaviour
    {
        public static TechPoolObjectManager ContTechPoolObjectManager;

        private void Awake()
        {
            ContTechPoolObjectManager = this;
        }

        public GameObject GetTechFromPool(string techType, int id)
        {
            switch (techType)
            {
                case "Critter":
                {
                    var tech = TechCritterPools.CritterPools.GetCritterById(id);
                    return tech;
                }
                case "Construct":
                {
                    var tech = TechConstructPools.ConstructPools.GetConstructById(id);
                    return tech;
                }
                case "Projectile":
                {
                    var tech = ProjectilePoolManager.poolProj.projPools[id].GetPooledGameObject();
                    return tech;
                }
                default:
                    return null;
            }
        }
    }
}