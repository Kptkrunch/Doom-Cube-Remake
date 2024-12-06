using System;
using MoreMountains.Feedbacks;
using UnityEngine;

namespace Weapons.WeaponModifiers
{
    public class ProjFrameWithTimer : MonoBehaviour
    {
        public float activeInterval, coolDownTimer;
        public int pid;
        public GameObject projectileFrame;

        private float _activeTimer;
        private bool _isActive;

        private void Start()
        {
            projectileFrame.SetActive(false);
            _isActive = false;
        }
        
        private void OnEnable()
        {
            var p = WeaponSfxGroupController.Instance.sfxControllers[pid].player;
            p.FeedbacksList[3].Play(transform.position);
        }

        private void OnDisable()
        {
            var p = WeaponSfxGroupController.Instance.sfxControllers[pid].player;
            if (p.FeedbacksList[3].IsPlaying) p.FeedbacksList[3].Stop(transform.position);        }

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