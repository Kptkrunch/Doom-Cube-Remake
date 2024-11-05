using UnityEngine;

namespace GenUtilsAndTools
{
    public class GrowShrinkObj : MonoBehaviour
    {
        public Vector3 maxSize;
        public float growShrinkSpeed;
        public float staySizeInterval;

        private Vector3 _targetSize;
        private float _staySizeTimer;


        private void Start()
        {
            _targetSize = maxSize;
            transform.localScale = Vector3.zero;
            _staySizeTimer = staySizeInterval;
        }

        private void OnEnable()
        {
            transform.localScale = Vector3.zero;
            _targetSize = maxSize;
            _staySizeTimer = staySizeInterval;
        }

        private void Update()
        {
            transform.localScale =
                Vector3.MoveTowards(transform.localScale, _targetSize, growShrinkSpeed * Time.deltaTime);
            _staySizeTimer -= Time.deltaTime;
            if (_staySizeTimer <= 0) _targetSize = Vector3.zero;
        }
    }
}