using System;
using System.Collections;
using Controllers;
using UnityEngine;

namespace MutantsAndRobots
{
    public class AnyAttack : MonoBehaviour
    {
        public float damage, hits, hitsTimer;
        public string damageType;
        public ParticleSystem attackParticleSystem;
        public GameObject parent;
        private EnemyController _enemyController;
        private float _hitInterval;

        private void FixedUpdate()
        {
            hitsTimer -= Time.deltaTime;
            if (!attackParticleSystem.isPlaying)
            {
                Debug.Log("done attacking");
                parent.gameObject.SetActive(false);
            }
            else
            {
                // Debug.Log("No particle system to check yet");
            }
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.CompareTag("Enemy") && hitsTimer <= 0)
            {
                hitsTimer = _hitInterval;
                collision.GetComponent<EnemyController>().TakeDamage(damage, damageType);
            }
        }

        public IEnumerator SpecAttack(EnemyController enemy)
        {
            attackParticleSystem.Play();
            for (var i = 0; i < hits; i++)
            {
                yield return new WaitForSeconds(hitsTimer);
                enemy.TakeDamage(damage, damageType);
            }

            if (!attackParticleSystem.isPlaying) StopCoroutine(SpecAttack(_enemyController));
        }
    }
}