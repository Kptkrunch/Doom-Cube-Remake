using System;
using Controllers.Pools;
using Damagers;
using UnityEngine;

namespace Weapons.Projectiles
{
    public class Mine : MonoBehaviour
    {
        public int expIndex;
        public GameObject parent;
        public bool hasLifetime, hasFuse, onContact;
        public int damage, expRadius;
        public float fuseTimer = 3;
        public float lifeTimer = 10;
        private float _fuseTimer, _lifeTimer;

        private void Start()
        {
            _fuseTimer = fuseTimer;
            _lifeTimer = lifeTimer;
        }

        private void FixedUpdate()
        {
            if (hasLifetime) _lifeTimer -= Time.deltaTime;
            if (hasFuse) _fuseTimer -= Time.deltaTime;

            if (hasLifetime && _lifeTimer <= 0)
            {
                _lifeTimer = lifeTimer;
                parent.gameObject.SetActive(false);
            }
            else if (hasFuse && _fuseTimer <= 0)
            {
                _fuseTimer = fuseTimer;
                Detonate();
                parent.gameObject.SetActive(false);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Enemy")) Detonate();
        }

        private void Detonate()
        {
            _fuseTimer = fuseTimer;
            _lifeTimer = lifeTimer;
            var exp = ProjectilePoolManager2.poolProj.projPools[expIndex].GetPooledGameObject();
            var damager = exp.GetComponentInChildren<EExplosionDamager>();
            damager.damage = damage;
            damager.blastRadiusCollider.radius = expRadius;
            exp.transform.position = transform.position;
            exp.gameObject.SetActive(true);
            parent.gameObject.SetActive(false);
        }
    }
}