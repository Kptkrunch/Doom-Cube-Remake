using System.Collections;
using Controllers;
using MoreMountains.Tools;
using UnityEngine;

namespace Weapons.SpecificWeapons
{
    public class MineSlayer : PrefabBasedWeapon
    {
        public static MineSlayer Instance;
        public MMSimpleObjectPooler minePool, explosionPool, muzzleFlashPool;

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
                yield return new WaitForSeconds(stats.weaponLvls[stats.lvl].rateOfFire);
                Fire();
            }

            yield return new WaitForSeconds(stats.weaponLvls[stats.lvl].coolDown);
            CanFire = true;
        }

        protected override void Fire()
        {
            var flash = muzzleFlashPool.GetPooledGameObject();
            flash.transform.position = transform.position;
            flash.SetActive(true);
            var mine = minePool.GetPooledGameObject();
            mine.transform.position = transform.position;
            mine.SetActive(true);
            juiceManager.TriggerFeedback(GenericJuiceManager.FeedbackType.Firing);
        }

        private void SetStats()
        {
            FireInterval = stats.weaponLvls[stats.lvl].rateOfFire;
            ReloadInterval = stats.weaponLvls[stats.lvl].coolDown;
            Ammo = stats.weaponLvls[stats.lvl].ammo;
        }
    }
}