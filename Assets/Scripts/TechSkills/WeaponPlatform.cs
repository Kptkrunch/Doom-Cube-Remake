using UnityEngine;

namespace Prefabs.Tech
{
    public class DeployableWeapon : MonoBehaviour
    {
        public bool canHeal, canDamage, canRepair;
        public float enemyDetectionRange = 10f;
        public float attackDistance = 2f;
        public float speed = 5f;
        public float healDistance = 5f;
        public float healAmount = 10f;
        public float activeTime = 30f;
        public int maxHits = 10;

        private Transform _weaponTransform;
        private GameObject[] _enemies;
        private GameObject[] _critters;
        private GameObject _currentTarget;
        private float _startTime;
        private int _hitCount;

        void Start()
        {
            _weaponTransform = transform;
            _enemies = GameObject.FindGameObjectsWithTag("Enemy");
            _critters = GameObject.FindGameObjectsWithTag("Critter");
            _startTime = Time.time;
            _hitCount = 0;
        }

        void Update()
        {
            // Check if the weapon has been active for too long or has been hit too many times
            if (Time.time - _startTime > activeTime || _hitCount >= maxHits)
            {
                gameObject.SetActive(false);
                return;
            }

            // Look for enemies
            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(_weaponTransform.position, enemyDetectionRange);
            int i = 0;
            while (i < hitColliders.Length)
            {
                if (hitColliders[i].CompareTag("Enemy"))
                {
                    // Attack the enemy
                    if (!_currentTarget || _currentTarget != hitColliders[i].gameObject)
                    {
                        _currentTarget = hitColliders[i].gameObject;
                    }
                    if (Vector3.Distance(_weaponTransform.position, _currentTarget.transform.position) > attackDistance)
                    {
                        _weaponTransform.Translate((_currentTarget.transform.position - _weaponTransform.position).normalized * (speed * Time.deltaTime));
                    }
                    else
                    {
                        // Attack the enemy
                        Debug.Log("Attacking enemy");
                        _hitCount++;
                    }
                    return;
                }
                i++;
            }

            if (canHeal)
            {
                // Check for companions within range
                foreach (GameObject companion in _critters)
                {
                    float distanceToCompanion = Vector3.Distance(_weaponTransform.position, companion.transform.position);
                    if (distanceToCompanion <= healDistance)
                    {
                        // Heal the companion
                    }
                }
            }

        }
    }
}

