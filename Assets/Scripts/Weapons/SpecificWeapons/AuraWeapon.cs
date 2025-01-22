using System;
using Controllers;
using Damagers;
using MoreMountains.Feedbacks;
using UnityEngine;
using UnityEngine.SocialPlatforms;

namespace Weapons.SpecificWeapons
{
    public class AuraWeapon : Weapon
    {
        public EAuraDamager auraDamager;
        [SerializeField] private CircleCollider2D auraCollider;
        [SerializeField] private Transform auraParticles;
        private float _sfxTimer, _sfxInterval;
        private void Start()
        {
            _sfxInterval = 10.0f;
            _sfxTimer = _sfxInterval;
            SetStats(); 
        }

        private void FixedUpdate()
        {
            _sfxTimer -= Time.deltaTime;
            if (_sfxTimer <= 0)
            {
                _sfxTimer = _sfxInterval;
                juiceManager.TriggerFeedback(GenericJuiceManager.FeedbackType.Firing);
            }
        }

        private void OnDisable()
        {
            juiceManager.StopFeedback(GenericJuiceManager.FeedbackType.Firing);
        }

        private void SetStats()
        {
            auraDamager.damage = stats.weaponLvls[stats.lvl].damage;
            auraDamager.damageInterval = stats.weaponLvls[stats.lvl].rateOfFire;
            auraCollider.radius = stats.weaponLvls[stats.lvl].range * 2;
            auraParticles.localScale = Vector3.one * stats.weaponLvls[stats.lvl].range;
        }

        public override void UpdateWeapon()
        {
            WeaponLevelUp();
            auraDamager.damage = stats.weaponLvls[stats.lvl].damage;
            auraCollider.radius = stats.weaponLvls[stats.lvl].range;
            auraDamager.transform.localScale =
                new Vector2(stats.weaponLvls[stats.lvl].range, stats.weaponLvls[stats.lvl].range);
            auraDamager.damageInterval = stats.weaponLvls[stats.lvl].rateOfFire;
        }
    }
}