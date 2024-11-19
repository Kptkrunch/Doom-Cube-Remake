using System;
using Controllers;
using UnityEngine;

namespace Weapons.WeaponModifiers
{
    public class BeamImpact : MonoBehaviour
    {
        public Weapon parent;
        public string damageType;
        private float _damageTimer, _damageInterval, _damage;

        private void Awake()
        {
            // eventually make sound follow beam as it moves
            MusicManager.Instance.sfxPlayerProjectiles.FeedbacksList[parent.stats.pid].Play(transform.position);
        }

        private void Start()
        {
            _damageInterval = parent.stats.weaponLvls[parent.stats.lvl].rateOfFire;
            _damageTimer = _damageInterval;
            _damage = parent.stats.weaponLvls[parent.stats.lvl].damage;
        }

        private void FixedUpdate()
        {
            _damageTimer -= Time.deltaTime;
        }

        private void OnDisable()
        {
            MusicManager.Instance.sfxPlayerProjectiles.FeedbacksList[parent.stats.pid].Stop(transform.position);
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.CompareTag("Enemy"))
                if (_damageTimer <= 0)
                {
                    Debug.Log("Damage?");
                    _damageTimer = _damageInterval;
                    collision.GetComponent<EnemyController>().TakeDamage(_damage, damageType);
                }
        }
    }
}