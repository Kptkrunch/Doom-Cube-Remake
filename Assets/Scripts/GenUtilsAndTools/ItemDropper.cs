using Controllers.Pools;
using JetBrains.Annotations;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GenUtilsAndTools
{
    public class ItemDropper : MonoBehaviour
    {
        public float dropChance;
        public bool meat, metal, mineral, plastic, energy, exp;
        [CanBeNull] public GameObject itemDrop;

        private float _dropCheckValue;

        private void OnEnable()
        {
            _dropCheckValue = Random.Range(0, 100);
        }

        public void DropItem()
        {
            Instantiate(itemDrop, transform.position, Quaternion.identity);
        }

        public void DropResource()
        {
            if (exp)
            {
                var res = ResourcePool.poolRes.poolExp.GetPooledGameObject();
                res.transform.position = gameObject.transform.position;
                res.gameObject.SetActive(true);
            }
            
            if (_dropCheckValue <= dropChance)
            {
                if (meat)
                {
                    var res = ResourcePool.poolRes.poolMeat.GetPooledGameObject();
                    res.transform.position = gameObject.transform.position;
                    res.gameObject.SetActive(true);
                }

                if (metal)
                {
                    var res = ResourcePool.poolRes.poolMetal.GetPooledGameObject();
                    res.transform.position = gameObject.transform.position;
                    res.gameObject.SetActive(true);
                }

                if (mineral)
                {
                    var res = ResourcePool.poolRes.poolMineral.GetPooledGameObject();
                    res.transform.position = gameObject.transform.position;
                    res.gameObject.SetActive(true);
                }

                if (plastic)
                {
                    var res = ResourcePool.poolRes.poolPlastic.GetPooledGameObject();
                    res.transform.position = gameObject.transform.position;
                    res.gameObject.SetActive(true);
                }

                if (energy)
                {
                    var res = ResourcePool.poolRes.poolEnergy.GetPooledGameObject();
                    res.transform.position = gameObject.transform.position;
                    res.gameObject.SetActive(true);
                }
            }
        }
    }
}
