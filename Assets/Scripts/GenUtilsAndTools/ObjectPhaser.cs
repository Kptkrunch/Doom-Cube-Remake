using UnityEngine;

namespace GenUtilsAndTools
{
    public class ObjectPhaser : MonoBehaviour
    {
        public GameObject obj;
        public float phaseOutInterval, phaseInInterval;
        private float _phaseOutTimer, _phaseInTimer;
        private bool _phasedIn = true;

        private void FixedUpdate()
        { 
            obj.SetActive(_phasedIn);
            PhaseObject();
        }

        private void PhaseObject()
        {
            if (_phasedIn)
            {
                _phaseOutTimer -= Time.deltaTime;
                if (_phaseOutTimer <= 0)
                {
                    _phasedIn = false;
                    _phaseInTimer = phaseInInterval;
                }
            } else if (!_phasedIn)
            {
                _phaseInTimer -= Time.deltaTime;
                if (_phaseInTimer <= 0)
                {
                    _phasedIn = true;
                    _phaseOutTimer = phaseOutInterval;
                }
            }
            
        }

        public void ResetPhasingOnHit()
        {
            _phasedIn = false;
            _phaseInTimer = phaseInInterval;
        }
    }
}
