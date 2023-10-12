using Controllers.Pools;
using UnityEngine;

namespace GenUtilsAndTools
{
    public class ResDropper : MonoBehaviour
    {
        public bool meat, metal, mineral, plastic, energy;
        
        public ResourcePool resourcePool;
        private GameObject _resDrop;

        private void Start()
        {
            resourcePool = ResourcePool.poolRes;
        }

        public void DropResource()
        {
            if (meat)
            {
                _resDrop = resourcePool.poolMeat.GetPooledGameObject();
                _resDrop.transform.position = transform.position;
                _resDrop.SetActive(true);
            } 
            if (metal)
            {
                _resDrop = resourcePool.poolMetal.GetPooledGameObject();
                _resDrop.transform.position = transform.position;
                _resDrop.SetActive(true);
            } 
            if (mineral)
            {
                _resDrop = resourcePool.poolMineral.GetPooledGameObject();
                _resDrop.transform.position = transform.position;
                _resDrop.SetActive(true);
            } 
            if (plastic)
            {
                _resDrop = resourcePool.poolPlastic.GetPooledGameObject();
                _resDrop.transform.position = transform.position;
                _resDrop.SetActive(true);
            } 
            if (energy)
            {
                _resDrop = resourcePool.poolEnergy.GetPooledGameObject();
                _resDrop.transform.position = transform.position;
                _resDrop.SetActive(true);
            }
        }
    }
}
