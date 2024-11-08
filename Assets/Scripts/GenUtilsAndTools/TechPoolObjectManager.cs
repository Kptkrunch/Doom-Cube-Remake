using Controllers.Pools;
using UnityEngine;

namespace GenUtilsAndTools
{
    public class TechPoolObjectManager : MonoBehaviour
    {
        public static TechPoolObjectManager ContTechPoolObjectManager;
        // private float _timer = .2f;

        private void Awake()
        {
            ContTechPoolObjectManager = this;
        }

        // private void Update()
        // {
        //     _timer -= Time.deltaTime;
        //     if (_timer <= 0) gameObject.SetActive(false);
        // }
        //
        // private void OnEnable()
        // {
        //     _timer = .2f;
        // }

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
                    var tech = TechProjectilePools.ProjectilePools.GetProjectileById(id);
                    return tech;
                }
                default:
                    return null;
            }
        }
    }
}