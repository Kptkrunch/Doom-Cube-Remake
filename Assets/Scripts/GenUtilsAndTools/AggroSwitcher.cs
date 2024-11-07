using Controllers;
using UnityEngine;

namespace GenUtilsAndTools
{
    public class AggroSwitcher : MonoBehaviour
    {
        public CircleCollider2D switchRadius;
        public Transform parent;
        public float aggroRadius;

        private void Start()
        {
            switchRadius.radius = aggroRadius;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Enemy")) collision.GetComponent<EnemyController>().target = parent;
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Enemy"))
                collision.GetComponent<EnemyController>().target = PlayerHealthController.contPHealth.transform;
        }
    }
}