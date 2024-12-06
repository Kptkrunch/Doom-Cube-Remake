using System;
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
        private void Start()
        {
            SetStats(); 
        }

        private void OnEnable()
        {
            // play the passive loop sound effect 0 is one-shot, 1 is hit, 2 is explosion sound, 3 is looping sound
            WeaponSfxGroupController.Instance.sfxControllers[stats.wid].player.FeedbacksList[3].Play(transform.position);
        }

        private void OnDisable()
        {
            WeaponSfxGroupController.Instance.sfxControllers[stats.wid].player.FeedbacksList[3].Stop(transform.position);
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