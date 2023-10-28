using Controllers.Pools;
using Damagers;
using UnityEngine;

namespace Weapons.Projectiles
{
    public class Mine : MonoBehaviour
    {
        public int expIndex;
        public GameObject parent;
        public bool hasLifetime, hasFuse;
        public int damage, expRadius;
        public float fuseTimer = 3;
        public float lifeTimer = 10;

        private void FixedUpdate()
        {
            if (hasLifetime) lifeTimer -= Time.deltaTime;
            if (hasFuse) fuseTimer -= Time.deltaTime;
        
            if (hasLifetime && lifeTimer <= 0)
            {
                parent.gameObject.SetActive(false);
            } else if (hasFuse && fuseTimer <= 0)
            {
                Detonate();
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Detonate();
        }

        private void Detonate()
        {
            var exp = ProjectilePoolManager2.poolProj.projPools[expIndex].GetPooledGameObject();
            var damager = exp.GetComponent<EExplosionDamager>();
            damager.damage = damage;
            damager.blastRadiusCollider.radius = expRadius;
            exp.transform.position = transform.position;
            exp.gameObject.SetActive(true);
            parent.gameObject.SetActive(false);
        }
    }
}
