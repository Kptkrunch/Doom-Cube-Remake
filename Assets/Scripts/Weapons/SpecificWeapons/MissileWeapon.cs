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
        private float _missleIndex;
        private bool _pinged;

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
            var targetSignal = enemy;
            for (var i = 0; i < Ammo; i++)
            {
                LaunchMissile(enemy.x + i * 2, targetSignal.y);
                yield return new WaitForSeconds(FireInterval);
            }

            yield return new WaitForSeconds(ReloadInterval);
            CanFire = true;
        }

        private void OnEnable()
        {
            StartCoroutine(RadarScan());
        }

        private void OnDisable()
        {
            StopCoroutine(RadarScan());
        }

        private IEnumerator RadarScan()
        {
            while (gameObject.activeSelf)
            {
                yield return new WaitForSeconds(.1f);
                lockLine.gameObject.transform.rotation = Quaternion.Euler(0f, 0f,
                    lockLine.gameObject.transform.rotation.eulerAngles.z +
                    stats.weaponLvls[stats.lvl].speed * stats.weaponLvls[stats.lvl].speed * Time.deltaTime * 50f);
            }
        }

        private IEnumerator RadarPing()
        {
            if (!_pinged)
            {
                var ping = ProjectilePoolManager.poolProj.projPools[missileIndex + 1].GetPooledGameObject();
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
            ReloadInterval = stats.weaponLvls[stats.lvl].coolDown;
            FireInterval = stats.weaponLvls[stats.lvl].rateOfFire;
        }

        private void LaunchMissile(float xValue, float yValue)
        {
            StartCoroutine(RadarPing());
            var missileTarget = new Vector2(xValue, yValue);
            var missile = ProjectilePoolManager2.poolProj.projPools[missileIndex].GetPooledGameObject();
            missile.transform.position = missileTarget;
            missile.GetComponentInChildren<SplineAnimate>().Play();
            missile.SetActive(true);
        }
    }
}