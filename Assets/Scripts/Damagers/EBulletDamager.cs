using UnityEngine;

namespace Damagers
{
    public class EBulletDamager : MonoBehaviour
    {
        public bool destroyOnCollision;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (destroyOnCollision && collision.CompareTag("Enemy")) gameObject.SetActive(false);

            if (destroyOnCollision && collision.CompareTag("WorldlyObject")) gameObject.SetActive(false);
        }
    }
}