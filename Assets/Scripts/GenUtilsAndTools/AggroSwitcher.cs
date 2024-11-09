using Controllers;
using UnityEngine;

namespace GenUtilsAndTools
{
    public class AggroSwitcher : MonoBehaviour
    {
        public CircleCollider2D switchRadius;
        public Transform parent;
        public float aggroRadius;
        private Transform _playerTransform;

        private void Awake()
        {
            _playerTransform = PlayerHealthController.contPHealth.transform;
        }

        private void Start()
        {
            switchRadius.radius = aggroRadius;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Enemy")) collision.GetComponent<EnemyController>().target = parent.transform;
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            _playerTransform = PlayerHealthController.contPHealth.transform;
            if (collision.CompareTag("Enemy"))
                collision.GetComponent<EnemyController>().target = _playerTransform;
        }
    }
}