using UnityEngine;

namespace AIScripts
{
    public class EnemyAI : MonoBehaviour
    {
        public float moveSpeed = 2f;
        public float attackDistance = 2f;
        public float retreatDistance = 5f;

        private Transform _playerTransform;
        private Transform _enemyTransform;
        private GameObject _currentBunker;

        void Start()
        {
            _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
            _enemyTransform = transform;
        }

        void Update()
        {
            if (_currentBunker == null)
            {
                // Move towards the player
                var position = _enemyTransform.position;
                position += (_playerTransform.position - position).normalized * (moveSpeed * Time.deltaTime);
                _enemyTransform.position = position;
            }
            else
            {
                // Take cover in the bunker
                var position = _enemyTransform.position;
                position += (_currentBunker.transform.position - position).normalized * (moveSpeed * Time.deltaTime);
                _enemyTransform.position = position;
            }

            float distanceToPlayer = Vector3.Distance(_enemyTransform.position, _playerTransform.position);

            if (distanceToPlayer <= attackDistance)
            {
                // Attack the player
                Debug.Log("Attacking player");
            }
            else if (distanceToPlayer > retreatDistance)
            {
                // Retreat from the player
                Debug.Log("Retreating from player");
            }
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag($"Bunker"))
            {
                // Take cover in the bunker
                _currentBunker = other.gameObject;
            }
        }

        void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag($"Bunker"))
            {
                // Leave the bunker
                _currentBunker = null;
            }
        }
    }
}