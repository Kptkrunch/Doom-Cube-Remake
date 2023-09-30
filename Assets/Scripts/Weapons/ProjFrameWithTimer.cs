using UnityEngine;

namespace Weapons
{
    public class ProjFrameWithTimer : MonoBehaviour
    {
        public float activeInterval;
        public GameObject projectileFrame;
        public float coolDownTimer;
        private float _activeTimer;
        private bool _isActive;
    
        void Start()
        {
            projectileFrame.SetActive(false);
            _isActive = false;
        }

        void Update()
        {
            _activeTimer -= Time.deltaTime;
            if (_activeTimer <= 0)
            {
                _activeTimer = activeInterval;
                if (!_isActive)
                {
                    projectileFrame.SetActive(true);
                    _isActive = true;
                    _activeTimer = activeInterval;
                }
                else
                {
                    _activeTimer = coolDownTimer;
                    projectileFrame.SetActive(false);
                    _isActive = false;
                }
            }
        }
    }
}