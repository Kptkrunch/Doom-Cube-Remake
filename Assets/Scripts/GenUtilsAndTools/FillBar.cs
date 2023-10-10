using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class FillBar : MonoBehaviour
    {
        public static FillBar fbInstance;
        public Slider fillBar;
        public float startingValue;
        
        protected float currentValue, maxValue;

        private void Awake()
        {
            fbInstance = this;
        }
        
        private void Start()
        {
            currentValue = startingValue;
            fillBar.maxValue = maxValue;
            fillBar.value = currentValue;
        }
    }
}
