using System.Collections.Generic;
using UI;
using UnityEngine;

namespace Controllers
{
    public class DamageNumberController : MonoBehaviour
    {
        public static DamageNumberController dnController;

        private void Awake()
        {
            dnController = this;
        }

        public DamageNumber damageNumber;
        public Transform damageNumberTransform;
        private readonly List<DamageNumber> _damageNumbers = new();

        public void ShowDamage(float damage, Vector3 location)
        {
            // var rounded = Mathf.RoundToInt(damage);
            var newDamage = GetFromPool();

            newDamage.Setup(damage);
            newDamage.gameObject.SetActive(true);

            newDamage.transform.position = location;
        }
    
    
        public DamageNumber GetFromPool()
        {
            DamageNumber pooledNumber;

            if (_damageNumbers.Count == 0)
            {
                pooledNumber = Instantiate(damageNumber, damageNumberTransform);
            }
            else
            {
                pooledNumber = _damageNumbers[0];
                _damageNumbers.RemoveAt(0);
            }
            return pooledNumber;
        }

        public void PlaceInPool(DamageNumber damageNumberToAdd)
        {
            damageNumberToAdd.gameObject.SetActive(false);
            _damageNumbers.Add(damageNumberToAdd);
        }
    }
}
