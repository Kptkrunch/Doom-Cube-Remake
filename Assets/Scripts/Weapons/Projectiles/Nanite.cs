using System;
using Controllers;
using GenUtilsAndTools;
using MoreMountains.Feedbacks;
using UnityEngine;

namespace Weapons.Projectiles
{
    public class Nanite : Transmogrifier
    {
        public ObjectPhaser phaser;
        public GenericJuiceManager juiceManager;

        private void Awake()
        {
            juiceManager.TriggerFeedback(GenericJuiceManager.FeedbackType.Idle);
        }


        private void OnDisable()
        { 
            juiceManager.StopFeedback(GenericJuiceManager.FeedbackType.Idle);
        }
    }
}