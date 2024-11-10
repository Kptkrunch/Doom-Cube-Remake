using System.Collections;
using Controllers.Pools;
using UnityEngine;

namespace EnemyStuff
{
    public class RangedEnemyModifier : MonoBehaviour
    {
        public int projectileIndex;
        public float range;
        public float rateOfFire;
        public float cooldown;
        public int ammo;
        private bool _canFire;
        private Transform _target;

        private void Awake()
        {
            GetComponent<CircleCollider2D>().radius = range;
        }

        private void FixedUpdate()
        {
            if (_target != null && _canFire) StopCoroutine(AttackLoop());
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player") || collision.CompareTag("Construct")) _target = collision.transform;
        }

        private IEnumerator AttackLoop()
        {
            _canFire = false;
            for (var i = 0; i < ammo; i++)
            {
                var proj = EnemyAttackPools.PoolEnemyAtk.attackList[projectileIndex].GetPooledGameObject();
                proj.transform.transform.position = transform.position;
                proj.gameObject.SetActive(true);
                yield return new WaitForSeconds(rateOfFire);
            }

            yield return new WaitForSeconds(cooldown);
            _canFire = true;
        }
    }
}