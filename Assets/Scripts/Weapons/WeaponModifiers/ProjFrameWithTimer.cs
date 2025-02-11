using System;
using Controllers;
using MoreMountains.Feedbacks;
using UnityEngine;
using UnityEngine.Serialization;

namespace Weapons.WeaponModifiers
{
    public class ProjFrameWithTimer : MonoBehaviour
    {
        public float coolDown, duration;
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
            juiceManager.StopFeedback(GenericJuiceManager.FeedbackType.Idle);
            juiceManager.StopFeedback(GenericJuiceManager.FeedbackType.Firing);
        }

        private void Update()
        {
            _activeTimer -= Time.deltaTime;
            switch (_activeTimer <= 0)
            {
                case false:
                    return;
            }

            _activeTimer = duration;
            if (!_isActive)
            {
                projectileFrame.SetActive(true);
                _isActive = true;
                _activeTimer = duration;
            }
            else
            {
                _activeTimer = coolDown;
                projectileFrame.SetActive(false);
                _isActive = false;
            }
        }

        public void UpdateIntervals(float thisDuration, float thisCoolDown)
        {
            coolDown = thisCoolDown;
            duration = thisDuration;
        }
    }
}