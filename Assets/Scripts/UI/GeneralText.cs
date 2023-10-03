using Controllers;
using TMPro;
using UnityEngine;

namespace UI
{
    public class GeneralText : MonoBehaviour
    {
        public TMP_Text genText;
        public float showInterval, floatSpeed;
        
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
                    GeneralTextController.generalTextControllerController.PlaceInPool(this);
                }
            }
        
            transform.position += Vector3.up * (floatSpeed * Time.deltaTime);
        }

        public void Setup(string newText)
        {
            _showTimer = showInterval;
            genText.text = newText;
        }
    }
}
