using UnityEngine;
using UnityEngine.UI;

namespace GenUtilsAndTools
{
    public class FillBar : MonoBehaviour
    {
        public Slider fillBar;
        public float startingValue;

        private float _currentValue;
        protected float maxValue;

        private void Awake()
        {
        }

        private void Start()
        {
            _currentValue = startingValue;
            fillBar.maxValue = maxValue;
            fillBar.value = _currentValue;
        }
    }
}