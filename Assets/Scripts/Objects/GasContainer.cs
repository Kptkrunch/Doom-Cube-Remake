using Controllers;
using UnityEngine;

namespace Objects
{
    public class GasContainer : WordlyObject
    {
        public bool maybeHookedUpToGas;
        
        [Header("Explosion and leaking gas particles")]
        public GameObject
        expParticle,
        natGasParticle;

        [SerializeField] private LayerMask enemyLayer;
        [SerializeField] private LayerMask objectLayer;

        [SerializeField] private float aoeRadius, damage;
        private void Start()
        {
            if (maybeHookedUpToGas)
            {
                IsMaybeHookedUpToGas();
            }
        }
        
        private void IsMaybeHookedUpToGas()
        {
            var randomNumber = Random.Range(0, 100);
            if (randomNumber % 2 == 0)
            {
                maybeHookedUpToGas = true;
            }
        }

        public void GasLeak()
        {
            natGasParticle.SetActive(true);
        }

        public void Detonate()
        {
            expParticle.SetActive(true);
            var position = transform.position;
            var enemies = Physics2D.OverlapCircleAll(position, aoeRadius, enemyLayer);
            var objects = Physics2D.OverlapCircleAll(position, aoeRadius, objectLayer);
            var maxTargets = enemies.Length;
            if (objects.Length > enemies.Length)
            {
                maxTargets = objects.Length;
            }
            for (var i = 0; i < maxTargets; i++)
            {
                if (enemies[i])
                {
                    enemies[i].GetComponent<EnemyController>().TakeDamage(damage);
                }

                if (objects[i])
                {
                    objects[i].GetComponent<WordlyObject>().TakeDamage(damage);
                }
            }
        }
    }
}
