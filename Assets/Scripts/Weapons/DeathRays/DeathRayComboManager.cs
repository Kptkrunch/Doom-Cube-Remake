using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Weapons.DeathRays
{
    public class DeathRayComboManager : MonoBehaviour
    {
        public static DeathRayComboManager contCombo;
        public List<DeathRay> deathRays;
        public int maxBeamCharges = 3, beamCharges = 3;
        public float hitWindowTime = 0.25f;
        public float chargeRegenTime = .25f, chargeRestored;

        private int _currentHitIndex;
        private float _lastHitTime;
        private Gamepad _gamepad;

        private void Awake()
        {
            contCombo = this;
            _gamepad = Gamepad.current;
        }

        private void Start()
        {
            _currentHitIndex = 0;
            _lastHitTime = Time.time - hitWindowTime;
        }

        private void Update()
        {
            if (_currentHitIndex == 3) _currentHitIndex = 0;
            if (beamCharges > maxBeamCharges) beamCharges = maxBeamCharges;
            if (beamCharges < maxBeamCharges && Time.time - chargeRegenTime > chargeRestored)
            {
                beamCharges++;
                chargeRestored = Time.time;
            }

            if (_gamepad == null) return;
            if (Gamepad.current.rightTrigger.wasPressedThisFrame) ActivateCombo();
        }

        public void ActivateCombo()
        {
            if (Time.time - _lastHitTime > hitWindowTime && beamCharges > 0)
            {
                _currentHitIndex++;
                beamCharges--;
                _lastHitTime = Time.time;

                if (_currentHitIndex == 1)
                {
                    deathRays[0].FireBeam();

                    return;
                }

                if (_currentHitIndex == 2 && deathRays[1].isActiveAndEnabled)
                {
                    deathRays[1].FireBeam();
                    return;
                }

                if (_currentHitIndex == 3 && deathRays[2].isActiveAndEnabled)
                {
                    deathRays[2].FireBeam();
                    return;
                }
            }

            // Combo failed
            _currentHitIndex = 0;
        }
    }
}