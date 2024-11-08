using Controllers.Pools;
using UnityEngine;
using UnityEngine.Serialization;

namespace TechSkills
{
    public class TurretBottomSectionTech : Tech
    {
        public int pid;
        public Transform rotatingTopTransform;
        public float rotationSpeed = 10f, fireRate = 1f, range = 10f;
        
        private CircleCollider2D _circleCollider;
        private Transform _target;
        private float _nextFire;

        private void Awake()
        {
            rotatingTopTransform = gameObject.transform;
        }
        private void Start()
        {
            _circleCollider = GetComponent<CircleCollider2D>();
            _circleCollider.radius = range;
        }

        private void Update()
        {
            if (_target != null)
            {
                var direction = _target.position - rotatingTopTransform.position;
                var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
                var targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
                rotatingTopTransform.rotation = Quaternion.Slerp(rotatingTopTransform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

                if (Time.time > _nextFire)
                {
                    Fire();
                    _nextFire = Time.time + fireRate;
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Enemy")) _target = collision.transform;
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Enemy")) _target = null;
        }

        private void Fire()
        {
            var projectile = ProjectilePoolManager2.poolProj.projPools[pid].GetPooledGameObject();
            projectile.transform.rotation = rotatingTopTransform.rotation;
            projectile.SetActive(true);
            projectile.transform.position = rotatingTopTransform.position;
        }
    }
}