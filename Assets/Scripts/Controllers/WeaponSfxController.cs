using System.Collections.Generic;
using MoreMountains.Feedbacks;
using MoreMountains.Tools;
using UnityEngine;

namespace Controllers
{
    public class WeaponSfxController : MonoBehaviour
    {
        
        public string weaponName;
        public int wid;
        public MMF_Player player;
        private List<MMF_Feedback> _sfxFeedbacksList = new();

        private void Awake()
        {
            _sfxFeedbacksList = player.FeedbacksList;
        }
        
        public void PlayMuzzleSound()
        {
            _sfxFeedbacksList[0].Play(transform.position);
        }

        public void PlayProjectileSound()
        {
            _sfxFeedbacksList[1].Play(transform.position);
        }

        public void PlayEnemyHitSound()
        {
            _sfxFeedbacksList[2].Play(transform.position);
        }

        private void PlaySound(MMSoundManagerSound sound)
        {
            if (sound.Source != null)
            {
                sound.Source.Play();
            }
        }
    }
}