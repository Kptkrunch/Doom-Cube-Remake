using UnityEngine;

namespace AIScripts
{
    public class TurretRotation : MonoBehaviour
    {
        public Transform topTransform;
        public float rotationSpeed = 5f;

        void Update()
        {
            GameObject enemy = GetComponent<EnemyDetection>().FindClosestEnemy();
            if (enemy != null && GetComponent<EnemyDetection>().IsEnemyInCone(enemy))
            {
                Vector2 direction = enemy.transform.position - topTransform.position;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
                Quaternion targetRotation = Quaternion.Euler(0f, 0f, angle);
                topTransform.rotation = Quaternion.RotateTowards(topTransform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        }
    }
}