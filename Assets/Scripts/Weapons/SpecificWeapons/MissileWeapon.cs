using System;
using System.Collections;
using Controllers.Pools;
using UnityEngine;
using UnityEngine.Splines;
using Weapons.WeaponModifiers;

namespace Weapons.SpecificWeapons
{
    public class MissileWeapon : PrefabBasedWeapon
    {
        public int missileIndex;
        public Vector2 enemy;
        public LockLine lockLine;
        private float _pingInterval, _missleIndex;
        private bool _pinged;

        private void Start()
        {
            SetStats();
        }

        private void OnEnable()
        {
            StartCoroutine(RadarScan());
        }

        private void OnDisable()
        {
            StopCoroutine(RadarScan());
        }

        IEnumerator RadarScan()
        {
            while (gameObject.activeSelf)
            {
                yield return new WaitForSeconds(.1f);
                lockLine.gameObject.transform.rotation = Quaternion.Euler(0f, 0f,
                    lockLine.gameObject.transform.rotation.eulerAngles.z +
                    stats.weaponLvls[stats.lvl].speed * stats.weaponLvls[stats.lvl].speed * Time.deltaTime);
            }
        }

        IEnumerator RadarPing()
        {
            if (!_pinged)
            {
                var ping = ProjectilePoolManager2.poolProj.projPools[missileIndex + 1].GetPooledGameObject();
                ping.transform.position = transform.position;
                ping.SetActive(true);
                yield return new WaitForSeconds(ReloadInterval);
                _pinged = false;
            }
        }
        
        private void SetStats()
        {
            Ammo = stats.weaponLvls[stats.lvl].ammo;
            lockLine = GetComponentInChildren<LockLine>();
            _pingInterval = stats.weaponLvls[stats.lvl].duration;
            ReloadInterval = stats.weaponLvls[stats.lvl].coolDown;
            FireInterval = stats.weaponLvls[stats.lvl].rateOfFire;
        }

        protected override void Fire()
        {
            StartCoroutine(RadarPing());
            var missileTarget = new Vector2(enemy.x + _missleIndex, enemy.y);
            var missile = ProjectilePoolManager2.poolProj.projPools[2].GetPooledGameObject();
            missile.transform.position = missileTarget;
            missile.GetComponentInChildren<SplineAnimate>().Play();
            missile.SetActive(true);
        }
    }
}
