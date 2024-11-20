using System;
using GenUtilsAndTools;
using UnityEngine;

namespace Weapons.Projectiles
{
    public class Nanite : Transmogrifier
    {
        public ObjectPhaser phaser;

        private void Awake()
        {
            MusicManager.Instance.sfxPlayerProjectiles.FeedbacksList[pid].Play(transform.position);
            // play a particle on spawn
        }


        private void OnDisable()
        {
            MusicManager.Instance.sfxPlayerProjectiles2.FeedbacksList[pid].Play(transform.position);
        }
    }
}