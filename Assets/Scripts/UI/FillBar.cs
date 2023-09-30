using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class FillBar : MonoBehaviour
    {
        public static FillBar fbInstance;
        public Slider fillBar;
        public float startingValue;

        private void Awake()
        {
            fbInstance = this;
        }

        protected float currentValue;
        protected float maxValue;
        void Start()
        {
            currentValue = startingValue;
            fillBar.maxValue = maxValue;
            fillBar.value = currentValue;
        }

        public void IncreaseBar(float value)
        {
            currentValue += value;
            fillBar.value = currentValue;
        }
    }
}
