using System.Collections;
using UnityEngine;

namespace EnemyStuff
{
    public class Jumper : MonoBehaviour
    {
        public float moveSpeed = 2f;
        public float jumpSpeed = 5f;
        public float attackDistance = 2f;
        public float retreatDistance = 1f;

        private Transform _playerTransform;
        private Transform _enemyTransform;
        private bool _isJumping;

        private void Start()
        {
            _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
            _enemyTransform = transform;
        }

        private void Update()
        {
            var distanceToPlayer = Vector3.Distance(_enemyTransform.position, _playerTransform.position);

            if (distanceToPlayer <= attackDistance)
            {
                // Jump attack
                if (!_isJumping) StartCoroutine(JumpAttack());
            }
            else
                // Move towards the player
            {
                _enemyTransform.position += (_playerTransform.position - _enemyTransform.position).normalized *
                                            (moveSpeed * Time.deltaTime);
            }
        }

        private IEnumerator JumpAttack()
        {
            _isJumping = true;

            // Jump towards the player
            var position = _enemyTransform.position;
            var position1 = _playerTransform.position;
            var jumpDirection = (position1 - position).normalized;
            position += jumpDirection * (jumpSpeed * Time.deltaTime);

            // Retreat after the attack
            yield return new WaitForSeconds(0.5f);
            var retreatDirection = (position - position1).normalized;
            position += retreatDirection * retreatDistance;
            _enemyTransform.position = position;

            _isJumping = false;
        }
    }
}