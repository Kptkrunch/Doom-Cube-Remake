using Controllers;
using UnityEngine;
using UnityEngine.Serialization;

namespace GenUtilsAndTools
{
    public class EExplosionDamager : EnemyDamager
    {
        [SerializeField] private GameObject explosion;
        [SerializeField] private GameObject theNade;

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
