using Controllers;
using JetBrains.Annotations;
using UnityEngine;

namespace Damagers
{
    public class EExplosionDamager : EnemyDamager
    {
        [CanBeNull] [SerializeField] private GameObject explosion;
        [CanBeNull] [SerializeField] private GameObject theNade;

        protected override void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Enemy"))
            {
                collision.gameObject.GetComponent<EnemyController>().TakeDamage(damage);
            }
        }
        public void Detonate()
        {
            if (explosion && theNade)
            {
                theNade.gameObject.SetActive(false);
                explosion.gameObject.SetActive(true);
            }
        }
    }
}
