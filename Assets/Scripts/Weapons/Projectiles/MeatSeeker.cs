using System;
using Controllers.Pools;
using Damagers;
using UnityEngine;
using UnityEngine.Splines;

namespace Weapons.Projectiles
{
    public class MeatSeeker : MonoBehaviour
    {
        public int expIndex;
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
            var exp = ProjectilePoolManager.poolProj.projPools[expIndex].GetPooledGameObject();
            var damager = exp.GetComponent<EExplosionDamager>();
            damager.damage = damage;
            damager.blastRadiusCollider.radius = blastRadius;
            Debug.Log(damager.damage);
            Debug.Log(damager.blastRadiusCollider.radius);
            exp.gameObject.transform.position = transform.position;
            exp.SetActive(true);
            animator.Restart(false);
        }
    }
}