using UnityEngine;

namespace TechSkills
{
    public class BaseTurretTech : Tech
    {
        public Transform top;
        public float rotationSpeed = 10f;
        public float fireRate = 1f;
        public float range = 10f;
        public GameObject projectilePrefab;
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
                Vector3 direction = _target.position - top.position;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
                Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
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
            if (collision.CompareTag("Enemy"))
            {
                _target = collision.transform;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Enemy"))
            {
                _target = null;
            }
        }

        private void Fire()
        {
            GameObject projectile = Instantiate(projectilePrefab, top.position, top.rotation);
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            rb.velocity = top.up * 3f;
        }
    }
}