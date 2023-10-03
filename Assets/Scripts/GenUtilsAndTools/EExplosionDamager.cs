using Controllers;
using UnityEngine;

namespace GenUtilsAndTools
{
    public class EExplosionDamager : EnemyDamager
    {
        public GameObject explosion;
        public GameObject theNade;

        protected override void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Enemy"))
            {
                collision.gameObject.GetComponent<EnemyController>().TakeDamage(damage);
            }
        }
        public void Detonate()
        { 
            theNade.gameObject.SetActive(false);
            explosion.gameObject.SetActive(true);
        }
    }
}
