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
        private void Start()
        {
            currentValue = startingValue;
            fillBar.maxValue = maxValue;
            fillBar.value = currentValue;
        }
    }
}
