using UnityEngine;

namespace Weapons
{
    public class ProjFrameWithTimer : MonoBehaviour
    {
        public float activeInterval, coolDownTimer;
        public GameObject projectileFrame;
        
        private float _activeTimer;
        private bool _isActive;
    
        private void Start()
        {
            projectileFrame.SetActive(false);
            _isActive = false;
        }

        private void Update()
        {
            _activeTimer -= Time.deltaTime;
            switch ((_activeTimer <= 0))
            {
                case false:
                    return;
            }
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