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
        

        private void OnEnable()
        {
            // play the passive sound index on awake and end it on disabled
            parent.juiceManager.TriggerFeedback(GenericJuiceManager.FeedbackType.Firing);
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
            parent.juiceManager.StopFeedback(GenericJuiceManager.FeedbackType.Firing);
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.CompareTag("Enemy"))
                if (_damageTimer <= 0)
                {
                    _damageTimer = _damageInterval;
                    collision.GetComponent<EnemyController>().TakeDamage(_damage, damageType);
                }
        }
    }
}