using System.Collections;
using Controllers.Pools;
using MoreMountains.Feedbacks;
using MoreMountains.Tools;
using UnityEngine;
using Weapons.Projectiles;
using Random = UnityEngine.Random;

namespace Weapons.SpecificWeapons
{
    public class PlasmaBurster : PrefabBasedWeapon
    {
        private Vector2 _direction;
        private Quaternion _rotation;
        
        private void Awake()
        {
            SetStats();
            CanFire = true;
        }

        private void FixedUpdate()
        {
            if (CanFire) StartCoroutine(AttackLoop());
        }

        private IEnumerator AttackLoop()
        {
            CanFire = false;
            RandomDirection();
            Debug.Log(_direction);
            for (var i = 0; i < Ammo; i++)
            {
                yield return new WaitForSeconds(FireInterval);
                Fire();
            }

            yield return new WaitForSeconds(ReloadInterval);
            CanFire = true;
        }

        protected override void Fire()
        {
            base.Fire();
            var flash = MuzzleFlashPools.Instance.flashPools[stats.pid].GetPooledGameObject();
            flash.transform.position = transform.position;
            flash.SetActive(true);
            var proj = ProjectilePoolManager.poolProj.projPools[stats.pid].GetPooledGameObject();
            var theProj = proj.GetComponent<Projectile>();
            proj.transform.position = transform.position;
            theProj.pd.stats.direction = _direction;
            proj.SetActive(true);
            proj.transform.rotation = _rotation;
            WeaponSfxGroupController.Instance.sfxControllers[stats.wid].player.FeedbacksList[0].Play(transform.position);
        }

        private void SetStats()
        {
            FireInterval = stats.weaponLvls[stats.lvl].rateOfFire;
            ReloadInterval = stats.weaponLvls[stats.lvl].coolDown;
            Ammo = stats.weaponLvls[stats.lvl].ammo;
        }

        private void RandomDirection()
        {
            _direction = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)).normalized;
            var angle = Mathf.Atan2(_direction.x, _direction.y) * Mathf.Deg2Rad - 90;
            _rotation = Quaternion.Euler(0f, 0f, angle).normalized;
        }
    }
}