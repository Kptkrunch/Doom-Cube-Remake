using TMPro;
using UnityEngine;

namespace UI
{
    public class DamageNumber : MonoBehaviour
    {
        public TMP_Text damageText;
        public float floatSpeed, showInterval;
        
        private float _showTimer;
    
        private void Start()
        {
            _showTimer = showInterval;
        }

        private void Update()
        {
            if (_showTimer > 0)
            {
                _showTimer -= Time.deltaTime;
                if (_showTimer <= 0)
                {
                    // DamageNumberController.contDmgNum.PlaceInPool(this);
                }
            }
        
            transform.position += Vector3.up * (floatSpeed * Time.deltaTime);
        }
    }
}
