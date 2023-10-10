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

        private Transform playerTransform;
        private Transform enemyTransform;
        private bool isJumping = false;

        void Start()
        {
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
            enemyTransform = transform;
        }

        void Update()
        {
            float distanceToPlayer = Vector3.Distance(enemyTransform.position, playerTransform.position);

            if (distanceToPlayer <= attackDistance)
            {
                // Jump attack
                if (!isJumping)
                {
                    StartCoroutine(JumpAttack());
                }
            }
            else
            {
                // Move towards the player
                enemyTransform.position += (playerTransform.position - enemyTransform.position).normalized * moveSpeed * Time.deltaTime;
            }
        }

        IEnumerator JumpAttack()
        {
            isJumping = true;

            // Jump towards the player
            Vector3 jumpDirection = (playerTransform.position - enemyTransform.position).normalized;
            enemyTransform.position += jumpDirection * jumpSpeed * Time.deltaTime;

            // Retreat after the attack
            yield return new WaitForSeconds(0.5f);
            Vector3 retreatDirection = (enemyTransform.position - playerTransform.position).normalized;
            enemyTransform.position += retreatDirection * retreatDistance;

            isJumping = false;
        }
    }
}