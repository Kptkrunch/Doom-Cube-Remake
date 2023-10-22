using System.Collections;
using Controllers.Pools;
using Damagers;
using UnityEngine;
using UnityEngine.Serialization;

namespace Weapons
{
    public class RapidFireWeapon : Weapon
    {
        public Transform firePoint;

        private float _fireInterval, _reloadInterval;
        [FormerlySerializedAs("_canFire")] public bool canFire;
        private Transform _target;
        private Vector2 _direction;

        void Start()
        {
            SetStats();
            canFire = true;
        }
        private void FixedUpdate()
        {
            if (canFire)
            {
                StartCoroutine(RapidFireRandomDirection());
            }
        }
        
        public void FireWeapon(Vector3 projSpawn, Vector3 targetPosition)
        {
            var spawnedBullet = ProjectilePoolManager.poolProj.projPools[0].GetPooledGameObject();
            Debug.Log(spawnedBullet);
            spawnedBullet.gameObject.SetActive(true);

            spawnedBullet.transform.position = projSpawn;
            spawnedBullet.gameObject.SetActive(true);
            spawnedBullet.gameObject.GetComponentInChildren<Projectile>().MoveProjectile(_direction, false);
        }

        IEnumerator RapidFireRandomDirection()
        {
            canFire = false;
            float angle = Random.Range(0, 360) * Mathf.Deg2Rad;
            Vector2 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
            _direction = direction.normalized;
            Quaternion rotation = Quaternion.Euler(0f, 0f, angle);

            for (int i = 0; i < stats[weaponLevel].numOfProj; i++) 
            {
                yield return new WaitForSeconds(_fireInterval);
                var projectile = ProjectilePoolManager.poolProj.projPools[0].GetPooledGameObject();
                projectile.transform.position = firePoint.transform.position;
                projectile.SetActive(true);
                projectile.transform.rotation = rotation;
                projectile.GetComponentInChildren<Projectile>().MoveProjectile(_direction, true);
            }

            yield return new WaitForSeconds(_reloadInterval);
            canFire = true;
        }
        
        private void SetStats()
        {
            _fireInterval = stats[weaponLevel].rateOfFire;
            _reloadInterval = stats[weaponLevel].cdr;
        }

        public override void UpdateWeapon()
        {
            UpdateProjectile();
        }

        private void UpdateProjectile()
        {
            ProjectilePoolManager.poolProj.projPools[0].GetPooledGameObject().GetComponent<EnemyDamager>().damage =
                stats[weaponLevel].damage;            
            
            ProjectilePoolManager.poolProj.projPools[0].GetPooledGameObject().GetComponent<Projectile>().lifeTimer =
                stats[weaponLevel].duration;            
            
            ProjectilePoolManager.poolProj.projPools[0].GetPooledGameObject().GetComponent<Projectile>().moveSpeed =
                stats[weaponLevel].projSpeed;
            
            ProjectilePoolManager.poolProj.projPools[0].GetPooledGameObject().transform.localScale =
            Vector3.one * stats[weaponLevel].size;
        }
    }
}
