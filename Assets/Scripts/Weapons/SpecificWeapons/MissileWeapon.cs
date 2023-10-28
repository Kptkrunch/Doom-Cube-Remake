using System.Collections;
using System.Collections.Generic;
using Controllers.Pools;
using UnityEngine;
using Weapons.Projectiles;
using Weapons.WeaponModifiers;

namespace Weapons.SpecificWeapons
{
    public class MissileWeapon : Weapon
    {
        public int missileIndex;
        public GameObject missileLauncher;
        public List<MeatSeeker> missileRack;
        public Vector2 enemy;
        public LockLine lockLine;
        private float _pingTimer, _pingInterval, _launchInterval, _launchTimer, _rateOfFire;
        private int _numOfMissiles;
        private bool _pinged;

        private void Start()
        {
            SetStats();
        }

        private void FixedUpdate()
        {
            RadarPing();
            if (_launchTimer <= 0)
            {
                _launchTimer = _launchInterval;
                MissileBarrage();
            }
        }

        private void MissileBarrage()
        {
            missileLauncher.gameObject.transform.position = enemy;
            for (var i = 0; i < missileRack.Count; i++)
            {
                if (i > _numOfMissiles) break;
                StartCoroutine(MissleLaunchTimer());
                missileRack[i].animator.gameObject.SetActive(true);
                missileRack[i].animator.Play();
            }
        }

        IEnumerator MissleLaunchTimer()
        {
            yield return new WaitForSeconds(_rateOfFire);
        }

        private void RadarScan()
        {
            lockLine.gameObject.transform.rotation = Quaternion.Euler(0f, 0f,
                lockLine.gameObject.transform.rotation.eulerAngles.z +
                stats[weaponLevel].projSpeed * stats[weaponLevel].projSpeed * Time.deltaTime);
        }

        private void RadarPing()
        {
            _pingTimer -= Time.deltaTime;

            if (_pingTimer <= 0)
            {
                _launchTimer -= Time.deltaTime;
                if (!_pinged)
                {
                    _pinged = true;
                    var ping = ProjectilePoolManager2.poolProj.projPools[missileIndex + 1].GetPooledGameObject();
                    ping.transform.position = transform.position;
                    ping.SetActive(true);
                }
            }
            RadarScan();
        }
        private void SetStats()
        {
            for (var i = 0; i < missileRack.Count; i++)
            {
                missileRack[i].damage = (int)stats[weaponLevel].damage;
                missileRack[i].blastRadius = stats[weaponLevel].size;
            }
            _numOfMissiles = (int)stats[weaponLevel].numOfProj;
            lockLine = GetComponentInChildren<LockLine>();
            _pingInterval = stats[weaponLevel].duration;
            _pingTimer = _pingInterval;
            _launchInterval = stats[weaponLevel].cdr;
            _launchTimer = _launchInterval;
            _rateOfFire = stats[weaponLevel].rateOfFire;
        }
    }
}
