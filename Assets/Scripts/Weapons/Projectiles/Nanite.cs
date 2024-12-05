using System;
using GenUtilsAndTools;
using MoreMountains.Feedbacks;
using UnityEngine;

namespace Weapons.Projectiles
{
    public class Nanite : Transmogrifier
    {
        public ObjectPhaser phaser;

        private void OnAwake()
        {
            WeaponSfxGroupController.Instance.sfxControllers[wid].player.FeedbacksList[0].Play(transform.position);
        }


        private void OnDisable()
        { 
            var p = WeaponSfxGroupController.Instance.sfxControllers[pid].player;
            if (p.FeedbacksList[3].IsPlaying) p.FeedbacksList[3].Stop(transform.position);
        }
    }
}