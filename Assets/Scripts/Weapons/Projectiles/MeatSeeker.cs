using Controllers;
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
        public GameObject parent, missileTrack;

        private void FixedUpdate()
        {
            if (animator.isActiveAndEnabled && !animator.IsPlaying) Detonate();
        }

        private void Detonate()
        {
            var exp = ProjectilePoolManager2.poolProj.projPools[pid].GetPooledGameObject();
            var damager = exp.GetComponent<EExplosionDamager>();
            damager.damage = damage;
            damager.blastRadiusCollider.radius = blastRadius;
            exp.gameObject.transform.position = transform.position;
            exp.SetActive(true);
            GenericShakeController.Instance.ShakeWeakStrongViolent("strong", transform);
            animator.Restart(false);
            missileTrack.SetActive(false); }

        private void OnDisable()
        {
            var exp = ProjectilePoolManager2.poolProj.projPools[pid].GetPooledGameObject();
            var damager = exp.GetComponent<EExplosionDamager>();
            damager.damage = damage;
            damager.blastRadiusCollider.radius = blastRadius;
            exp.gameObject.transform.position = transform.position;
            exp.SetActive(true);
            GenericShakeController.Instance.ShakeWeakStrongViolent("strong", transform);
            animator.Restart(false);
            missileTrack.SetActive(false);
        }
    }
}