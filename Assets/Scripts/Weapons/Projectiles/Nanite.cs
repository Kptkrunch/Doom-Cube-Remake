using Controllers;
using GenUtilsAndTools;
using UnityEngine;
using Weapons.SpecificWeapons;

namespace Weapons.Projectiles
{
    public class Nanite : Transmogrifier
    {
        public ObjectPhaser phaser;

        protected override void OnTriggerEnter2D(Collider2D collision)
        {
            base.OnTriggerEnter2D(collision);
            if (collision.CompareTag("Enemy"))
            {
                MutagenicNaniteCrystals.Instance.juiceManager.TriggerFeedback(GenericJuiceManager.FeedbackType.Hit);
                var hitParticle = MutagenicNaniteCrystals.Instance.hitParticlePool.GetPooledGameObject();
                hitParticle.transform.position = collision.transform.position;
                hitParticle.SetActive(true);
            }
        }
        
        private void OnEnable()
        {
            MutagenicNaniteCrystals.Instance.juiceManager.TriggerFeedback(GenericJuiceManager.FeedbackType.Firing);
            var phaseParticle = MutagenicNaniteCrystals.Instance.phaseParticlePool.GetPooledGameObject();
            phaseParticle.transform.position = transform.position;
            phaseParticle.gameObject.SetActive(true);
        }
        
        private void OnDisable()
        {
            phaser.Reset();
        }
    }
}