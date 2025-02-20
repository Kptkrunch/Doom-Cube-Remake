using Controllers;
using Damagers;
using UnityEngine;
using UnityEngine.Splines;
using Weapons.SpecificWeapons;

namespace Weapons.Projectiles
{
    public class MeatSeeker : MonoBehaviour
    {
        public int pid;
        public float damage;
        public float blastRadius;
        public SplineAnimate animator;
        public GameObject parent, missileTrack;

        private void FixedUpdate()
        {
            if (animator.isActiveAndEnabled && !animator.IsPlaying) Detonate();
        }

        private void Detonate()
        {
            var exp = MeatSeekingMissileLauncher.Instance.explosionPool.GetPooledGameObject();
            var damager = exp.GetComponent<EExplosionDamager>();
            damager.damage = damage;
            damager.blastRadiusCollider.radius = blastRadius;
            exp.gameObject.transform.position = transform.position;
            exp.SetActive(true);
            GenericShakeController.Instance.ShakeWeakStrongViolent("strong", transform);
            MeatSeekingMissileLauncher.Instance.juiceManager.TriggerFeedback(GenericJuiceManager.FeedbackType.Death);
            animator.Restart(false);
            missileTrack.SetActive(false); 
        }

        private void OnDisable()
        {
            MeatSeekingMissileLauncher.Instance.juiceManager.StopFeedback(GenericJuiceManager.FeedbackType.Death);
            MeatSeekingMissileLauncher.Instance.juiceManager.StopFeedback(GenericJuiceManager.FeedbackType.Firing);
            // idle is the radar ping
            MeatSeekingMissileLauncher.Instance.juiceManager.StopFeedback(GenericJuiceManager.FeedbackType.Idle);
        }
    }
}