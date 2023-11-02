using System.Collections;
using Controllers.Pools;
using UnityEngine;
using UnityEngine.Splines;
using Weapons.WeaponModifiers;

namespace Weapons.SpecificWeapons
{
    public class MissileWeapon : Weapon
    {
        public int missileIndex;
        public Vector2 enemy;
        public LockLine lockLine;
        private float _pingTimer, _pingInterval, _launchInterval, _launchTimer, _rateOfFire, _numOfMissiles;
        private bool _pinged, _canFire;

        private void Start()
        {
            SetStats();
        }

        private void FixedUpdate()
        {
            RadarPing();
            if (_canFire)
            {
                StartCoroutine(MissileBarrage());
            }
        }

        IEnumerator MissileBarrage()
        {
            _canFire = false;
            for (var i = 0; i < _numOfMissiles; i++)
            {
                var missleTarget = new Vector2(enemy.x + i, enemy.y);
                yield return new WaitForSeconds(_rateOfFire);
                var missile = ProjectilePoolManager2.poolProj.projPools[2].GetPooledGameObject();
                missile.transform.position = missleTarget;
                missile.GetComponentInChildren<SplineAnimate>().Play();
                missile.SetActive(true);
            }
            yield return new WaitForSeconds(_launchInterval);
            _canFire = true;
        }

        private void RadarScan()
        {
            lockLine.gameObject.transform.rotation = Quaternion.Euler(0f, 0f,
                lockLine.gameObject.transform.rotation.eulerAngles.z +
                stats.weaponLvls[stats.lvl].speed * stats.weaponLvls[stats.lvl].speed * Time.deltaTime);
        }

        private void RadarPing()
        {
            _pingTimer -= Time.deltaTime;

            if (_pingTimer <= 0)
            {
                _launchTimer -= Time.deltaTime;
                if (!_pinged)
                {
                    _pingTimer = _pingInterval;
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
            _canFire = true;
            _numOfMissiles = stats.weaponLvls[stats.lvl].ammo;
            lockLine = GetComponentInChildren<LockLine>();
            _pingInterval = stats.weaponLvls[stats.lvl].duration;
            _pingTimer = _pingInterval;
            _launchInterval = stats.weaponLvls[stats.lvl].coolDown;
            _launchTimer = _launchInterval;
            _rateOfFire = stats.weaponLvls[stats.lvl].rateOfFire;
        }
    }
}
