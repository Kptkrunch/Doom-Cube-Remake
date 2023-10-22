using Controllers;
using JetBrains.Annotations;
using UnityEngine;

namespace Damagers
{
    public class EExplosionDamager : EnemyDamager
    {
        public float explosionRadius;
        public CircleCollider2D blastRadiusCollider;
        [CanBeNull] [SerializeField] private GameObject explosion;
        [CanBeNull] [SerializeField] private GameObject theNade;

        protected override void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Enemy"))
            {
                Detonate();
                collision.gameObject.GetComponent<EnemyController>().TakeDamage(damage);
            }
        }
        public void Detonate()
        {
            blastRadiusCollider.radius = explosionRadius;
            if (explosion && theNade)
            {
                theNade.gameObject.SetActive(false);
                explosion.gameObject.SetActive(true);
            }
        }
    }
}
