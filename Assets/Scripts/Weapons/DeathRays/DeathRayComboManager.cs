using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace Weapons.DeathRays
{
    public class DeathRayComboManager : MonoBehaviour
    {
        public static DeathRayComboManager contCombo;
        public List<DeathRay> deathRays;
        public int maxBeamCharges = 3, beamCharges;
        public float hitWindowTime;
        public float chargeRegenTime;
        private int _currentHitIndex;
        private float _lastHitTime, _lastRestoredChargeTime;
        private Gamepad _gamepad;

        private void Awake()
        {
            contCombo = this;
            _gamepad = Gamepad.current;
            beamCharges = maxBeamCharges;
        }

        private void Start()
        {
            _currentHitIndex = 0;
            _lastHitTime = Time.time;
            _lastRestoredChargeTime = Time.time;
        }

        private void Update()
        {
            if (_currentHitIndex >= 3)
            {
                _lastHitTime = Time.time;
                _currentHitIndex = 0;
            }
            if (beamCharges > maxBeamCharges) beamCharges = maxBeamCharges;
            if (beamCharges < maxBeamCharges && Time.time - chargeRegenTime > _lastRestoredChargeTime)
            {
                beamCharges++;
                _lastRestoredChargeTime = Time.time;
                // MusicManager.Instance.sfxUIButtons.FeedbacksList[0].Play(transform.position);
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