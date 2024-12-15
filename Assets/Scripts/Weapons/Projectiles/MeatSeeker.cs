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
            if (animator.ElapsedTime >= 1.0f)
            {
                parent.SetActive(false);
            }
        }

        private void OnDisable()
        {
            var exp = ProjectilePoolManager2.poolProj.projPools[pid].GetPooledGameObject();
            Debug.Log("name: " + exp.name);
            var damager = exp.GetComponentInChildren<EExplosionDamager>();
            WeaponSfxGroupController.Instance.sfxControllers[pid].player.FeedbacksList[2].Play(transform.position);            
            if (!exp || !damager)
            {
                Debug.Log("no exp or damager");
                return;
            }
            exp.gameObject.transform.position = transform.position;
            exp.SetActive(true);
            damager.damage = damage;
            damager.blastRadiusCollider.radius = blastRadius;
        }
    }
}