using UnityEngine;

namespace AIScripts
{
    public class Follow : MonoBehaviour
    {
        public Transform player;
        public float moveSpeed = 2.0f;
        public float playerDistance = 2.0f;

        private void FixedUpdate()
        {
            CheckAndMove();
        }
    
        private void CheckAndMove()
        {
            Vector3 targetPosition = player.position - player.forward * playerDistance;
            transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }
}
