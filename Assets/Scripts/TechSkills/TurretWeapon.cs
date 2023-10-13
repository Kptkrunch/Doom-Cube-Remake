using Controllers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TechSkills
{
    public class TurretWeapon : MonoBehaviour
    {
        public Transform baseTransform;
        public Transform topTransform;
        public float detectionRange = 5f;
        public float detectionAngle = 45f;
        public GameObject projectilePrefab;
        public Transform[] firePoints;
        public float fireRate = 1f;
        public float nextFireTime = 0f;
        public float moveSpeed = 3f;

        private int _currentFirePointIndex;
        public bool rapidFire, threeWayShot, scatterShot, threeRoundBurst;
        private EnemyDetection _enemyDetection;

        private void Start()
        {
            _enemyDetection = GetComponent<EnemyDetection>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Enemy"))
            {
                Vector2 direction = collision.GetComponent<EnemyController>().transform.position -
                                    topTransform.position;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

                if (Mathf.Abs(angle) <= 15f)
                {
                    Quaternion targetRotation = Quaternion.Euler(0f, 0f, angle);
                    topTransform.rotation =
                        Quaternion.RotateTowards(topTransform.rotation, targetRotation, Time.deltaTime * 180f);

                    if (Time.time >= nextFireTime)
                    {
                        nextFireTime = Time.time + 1f / fireRate;

                        if (rapidFire)
                        {
                            Fire(firePoints[_currentFirePointIndex]);
                            Fire(firePoints[(_currentFirePointIndex + 1) % firePoints.Length]);
                        }
                        else if (threeWayShot)
                        {
                            Fire(firePoints[_currentFirePointIndex], angle);
                            Fire(firePoints[_currentFirePointIndex], angle + 15f);
                            Fire(firePoints[_currentFirePointIndex], angle - 15f);
                            Fire(firePoints[(_currentFirePointIndex + 1) % firePoints.Length], angle);
                            Fire(firePoints[(_currentFirePointIndex + 1) % firePoints.Length], angle + 15f);
                            Fire(firePoints[(_currentFirePointIndex + 1) % firePoints.Length], angle - 15f);
                        }
                        else if (scatterShot)
                        {
                            for (int i = 0; i < 10; i++)
                            {
                                float randomAngle = Random.Range(-30f, 30f);
                                GameObject projectile = Instantiate(projectilePrefab,
                                    firePoints[_currentFirePointIndex].position,
                                    firePoints[_currentFirePointIndex].rotation *
                                    Quaternion.Euler(0f, 0f, 0f + randomAngle));
                                direction = projectile.transform.up;
                                projectile.GetComponent<Rigidbody2D>().velocity = direction.normalized * moveSpeed;
                            }
                        }
                        else if (threeRoundBurst)
                        {
                            Fire(firePoints[_currentFirePointIndex]);
                            Fire(firePoints[(_currentFirePointIndex + 1) % firePoints.Length]);
                            nextFireTime += 0.2f;
                            Fire(firePoints[_currentFirePointIndex]);
                            Fire(firePoints[(_currentFirePointIndex + 1) % firePoints.Length]);
                            nextFireTime += 0.2f;
                            Fire(firePoints[_currentFirePointIndex]);
                            Fire(firePoints[(_currentFirePointIndex + 1) % firePoints.Length]);
                        }
                        else
                        {
                            Fire(firePoints[_currentFirePointIndex]);
                            Fire(firePoints[(_currentFirePointIndex + 1) % firePoints.Length]);
                        }

                        _currentFirePointIndex++;
                        if (_currentFirePointIndex >= firePoints.Length)
                        {
                            _currentFirePointIndex = 0;
                        }
                    }
                }
            }
        }

        void Fire(Transform firePoint, float angleOffset = 0f)
        {
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation * Quaternion.Euler(0f, 0f, angleOffset));
            Vector2 direction = projectile.transform.up;
            projectile.GetComponent<Rigidbody2D>().velocity = direction.normalized * moveSpeed;
        }

        private bool IsEnemyInCone(GameObject enemy)
        {
            Vector2 directionToEnemy = enemy.transform.position - topTransform.position;
            float angleToEnemy = Vector2.Angle(directionToEnemy, topTransform.up);

            if (angleToEnemy <= detectionAngle / 2f && directionToEnemy.magnitude <= detectionRange)
            {
                return true;
            }

            return false;
        }
    }
}