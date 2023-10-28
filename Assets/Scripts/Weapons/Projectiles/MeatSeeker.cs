using Controllers.Pools;
using Damagers;
using UnityEngine;
using UnityEngine.Splines;

namespace Weapons.Projectiles
{
    public class MeatSeeker : MonoBehaviour
    {
        public int expIndex;
        public int damage;
        public float blastRadius;
        public SplineAnimate animator;

        private void FixedUpdate()
        {
            if (!animator.IsPlaying)
            {
                var exp = ProjectilePoolManager2.poolProj.projPools[expIndex].GetPooledGameObject();
                exp.GetComponent<EExplosionDamager>().damage = damage;
                exp.GetComponent<EExplosionDamager>().blastRadiusCollider.radius = blastRadius;
                exp.gameObject.transform.position = transform.position;
                exp.SetActive(true);
                animator.gameObject.SetActive(false);
                animator.Restart(false);
            } 
        }
    }
}
