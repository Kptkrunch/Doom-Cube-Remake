using Controllers;
using TMPro;
using UnityEngine;

namespace UI
{
    public class DamageNumber : MonoBehaviour
    {
        public TMP_Text damageText;
        public float showInterval;
        private float _showTimer;
        public float floatSpeed;
    
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
                    DamageNumberController.dnController.PlaceInPool(this);
                }
            }
        
            transform.position += Vector3.up * (floatSpeed * Time.deltaTime);
        }

        public void Setup(int damage)
        {
            _showTimer = showInterval;
            damageText.text = damage.ToString();
        }
    }
}
