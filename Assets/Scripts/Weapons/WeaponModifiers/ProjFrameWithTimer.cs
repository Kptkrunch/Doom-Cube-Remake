using System;
using Controllers;
using MoreMountains.Feedbacks;
using UnityEngine;

namespace Weapons.WeaponModifiers
{
    public class ProjFrameWithTimer : MonoBehaviour
    {
        public float activeInterval, coolDownTimer;
        public int pid;
        public GameObject projectileFrame;
        public GenericJuiceManager juiceManager;

        private float _activeTimer;
        private bool _isActive;

        private void Start()
        {
            projectileFrame.SetActive(false);
            _isActive = false;
        }
        
        private void OnEnable()
        {
            if(juiceManager) juiceManager.TriggerFeedback(GenericJuiceManager.FeedbackType.Idle);
        }

        private void OnDisable()
        {
            if(juiceManager) juiceManager.StopFeedback(GenericJuiceManager.FeedbackType.Idle);
        }

        private void Update()
        {
            _activeTimer -= Time.deltaTime;
            switch (_activeTimer <= 0)
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