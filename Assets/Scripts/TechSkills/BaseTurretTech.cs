using Controllers.Pools;
using UnityEngine;

namespace TechSkills
{
    public class BaseTurretTech : Tech
    {
        public Transform top;
        public int pid;
        public float rotationSpeed = 10f, fireRate = 1f, range = 10f;
        private CircleCollider2D _circleCollider;
        private Transform _target;
        private float _nextFire;

        private void Start()
        {
            _circleCollider = GetComponent<CircleCollider2D>();
            _circleCollider.radius = range;
        }

        private void Update()
        {
            if (_target != null)
            {
                var direction = _target.position - top.position;
                var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
                var targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
                top.rotation = Quaternion.Slerp(top.rotation, targetRotation, rotationSpeed * Time.deltaTime);

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
            var rb = projectile.GetComponent<Rigidbody2D>();
            rb.velocity = top.up * 3f;
        }
    }
}