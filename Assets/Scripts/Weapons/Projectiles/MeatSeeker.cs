using Controllers.Pools;
using Damagers;
using UnityEngine;
using UnityEngine.Splines;

namespace Weapons.Projectiles
{
    public class MeatSeeker : MonoBehaviour
    {
        public int pid;
        public float damage;
        public float blastRadius;
        public SplineAnimate animator;
        public GameObject parent;

        private void FixedUpdate()
        {
            if (animator.isActiveAndEnabled && !animator.IsPlaying) parent.SetActive(false);
        }

        private void OnDisable()
        {
            var exp = ProjectilePoolManager2.poolProj.projPools[pid].GetPooledGameObject();
            var damager = exp.GetComponent<EExplosionDamager>();
            damager.damage = damage;
            damager.blastRadiusCollider.radius = blastRadius;
            exp.gameObject.transform.position = transform.position;
            exp.SetActive(true);
            // 2 is the index for explosion sound on the weapons sfx player
            WeaponSfxGroupController.Instance.sfxControllers[pid].player.FeedbacksList[2].Play(transform.position);            
            animator.Restart(false);
        }
        
        
    }
}