using System.Collections;
using Controllers;
using MoreMountains.Tools;
using UnityEngine;

namespace Weapons.SpecificWeapons
{
    public class MechanicalMeatSeperator : LobbedWeapon
    {
        public static MechanicalMeatSeperator Instance;
        public MMSimpleObjectPooler sawPool, hitPool, muzzleFlashPool;
        private Vector3 _direction;
        
        private void Awake()
        {
            Instance = this;
        }
        private void Start()
        {
            SetStats();
        }

        private void FixedUpdate()
        {
            if (CanFire) StartCoroutine(AttackLoop());
        }

        private IEnumerator AttackLoop()
        {
            CanFire = false;
            for (var i = 0; i < Ammo; i++)
            {
                LaunchSawBlade();
                yield return new WaitForSeconds(FireInterval);
            }

            yield return new WaitForSeconds(Cooldown);
            CanFire = true;
        }

        private void SetStats()
        {
            CanFire = true;
            RateOfFire = stats.weaponLvls[stats.lvl].rateOfFire;
            Cooldown = stats.weaponLvls[stats.lvl].coolDown;
            Ammo = stats.weaponLvls[stats.lvl].ammo;
        }

        private void LaunchSawBlade()
        {
            var flash = muzzleFlashPool.GetPooledGameObject();
            flash.transform.position = transform.position;
            flash.SetActive(true);
            var saw = sawPool.GetPooledGameObject();
            saw.transform.position = transform.position;
            saw.SetActive(true);
            juiceManager.TriggerFeedback(GenericJuiceManager.FeedbackType.Firing);
        }
    }
}
