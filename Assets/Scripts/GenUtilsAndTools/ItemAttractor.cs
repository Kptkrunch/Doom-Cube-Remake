using System;
using UnityEngine;

namespace GenUtilsAndTools
{
    public class ItemAttractor : MonoBehaviour
    {
        public static ItemAttractor itemAttractor;
        public GameObject overDriveParticle;
        public float pickupRadius, pullSpeed, cooldown, overDriveDuration;
        public CircleCollider2D pickupArea;
        private float _cooldownTimer, _overDriveTimer, _pullSpeedTemp;
        private bool _overDrive;
        private void Awake()
        {
            itemAttractor = this;
            _cooldownTimer = cooldown;
            _overDriveTimer = overDriveDuration;
            _pullSpeedTemp = pullSpeed;
            pickupArea.radius = pickupRadius;
        }

        private void FixedUpdate()
        {
            if (!_overDrive)
            {
                _cooldownTimer -= Time.fixedDeltaTime;
                if (_cooldownTimer <= 0)
                {
                    _cooldownTimer = cooldown;
                    _overDrive = true;
                    overDriveParticle.SetActive(true);
                    pickupArea.radius = 20f;
                    pullSpeed = 5f;
                }
            }

            if (_overDrive)
            {
                _overDriveTimer -= Time.fixedDeltaTime;
                if (_overDriveTimer <= 0)
                {
                    _overDriveTimer = overDriveDuration;
                    _overDrive = false;
                    overDriveParticle.SetActive(false);
                    pickupArea.radius = pickupRadius;
                    pullSpeed = _pullSpeedTemp;
                }
            }
            
            
            
            
        }
    }
}