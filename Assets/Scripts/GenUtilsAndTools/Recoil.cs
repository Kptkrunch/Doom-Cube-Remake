using UnityEngine;

namespace GenUtilsAndTools
{
    public class Recoil : MonoBehaviour
    {
        public float recoilDistance = 0.5f;
        public float recoilSpeed = 10f;
        public float returnSpeed = 5f;

        private Vector3 _originalPosition;
        private bool _isRecoiling;

        private void Start()
        {
            _originalPosition = transform.localPosition;
        }

        private void Update()
        {
            if (_isRecoiling)
            {
                transform.Translate(Vector3.back * (recoilSpeed * Time.deltaTime), Space.Self);

                if (Vector3.Distance(_originalPosition, transform.localPosition) >= recoilDistance)
                {
                    _isRecoiling = false;
                }
            }
            else
            {
                transform.localPosition = Vector3.Lerp(transform.localPosition, _originalPosition, returnSpeed * Time.deltaTime);
            }
        }

        public void RecoilObject()
        {
            _isRecoiling = true;
        }
    }
}