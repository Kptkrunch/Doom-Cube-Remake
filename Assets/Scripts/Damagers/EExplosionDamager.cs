using System;
using System.Collections;
using Controllers;
using UnityEngine;

namespace Damagers
{
    public class EExplosionDamager : EnemyDamager
    {
        public CircleCollider2D blastRadiusCollider;

        protected override void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Enemy"))
                collision.gameObject.GetComponent<EnemyController>().TakeDamage(damage, damageType);
        }

        private void OnEnable()
        {
            StartCoroutine(DisableObject());
        }

        private void OnDisable()
        {
            StopCoroutine(DisableObject());
        }

        private IEnumerator DisableObject()
        {
            yield return new WaitForSeconds(1f);
            gameObject.SetActive(false);
        }
    }
}