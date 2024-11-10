using System;
using System.Collections;
using Controllers.Pools;
using ScriptableObjs.TechWeaponsSOS;
using UnityEngine;
using Weapons.Projectiles;

namespace TechSkills
{
    public class TurretTopSectionTech : MonoBehaviour
    {
        private static readonly int GreyscaleBlend = Shader.PropertyToID("_GreyscaleBlend");
        public TechWeaponData turretWeaponData;
        public int pid;
        public float rotationSpeed, fireRate, range;
        public SpriteRenderer spriteRenderer;
        public CircleCollider2D collider2d;
        public float percentDamaged;
        private bool _canFire = true, _init;
        private Transform _target;
        private Vector3 _direction;
        private void Start()
        {
            if (!_init) SetStats();
        }

        private void Awake()
        {
            if (_init) ResetOrInitTurretTop();
        }
        
        private void FixedUpdate()
        {
            if (_canFire)
            {
                StartCoroutine(AutoAttackLoop());
            }

            MaybeRotateTurretTop();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Enemy"))
            {
                _target = collision.transform;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Enemy"))
            {
                _target = null;
            }
        }

        public IEnumerator AutoAttackLoop()
        {
            _canFire = false;

            if (_target != null)
            {
                var proj = ProjectilePoolManager.poolProj.projPools[pid].GetPooledGameObject();
                var theProj = proj.GetComponentInChildren<Projectile>();
                proj.transform.position = transform.position;
                proj.transform.rotation = transform.rotation;
                theProj.pd.stats.direction = _direction;
                proj.SetActive(true);
            }
            
            yield return new WaitForSeconds(fireRate);
            _canFire = true;
        }
        private void MaybeRotateTurretTop()
        {
            if (!_target) return;
            _direction = transform.position - _target.transform.position;
            var angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg - 90f;
            var targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
        
        public void UpdateShaderGreyscaleBlend()
        {
            if (spriteRenderer != null && percentDamaged > 0f)
            {
                spriteRenderer.sharedMaterial.SetFloat(GreyscaleBlend, percentDamaged);
            }
        }
        
        private void ResetOrInitTurretTop()
        {
            percentDamaged = 0f;
            _canFire = true;
            _target = null;
            _direction = Vector3.zero;
        }

        private void SetStats()
        {
            fireRate = turretWeaponData.fireRate;
            range = turretWeaponData.range;
            pid = turretWeaponData.pid;
            rotationSpeed = turretWeaponData.rotationSpeed;
            collider2d.radius = range;
            _init = true;
        }
    }
}
