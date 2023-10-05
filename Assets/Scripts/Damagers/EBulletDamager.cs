using UnityEngine;

namespace Damagers
{
    public class EBulletDamager : MonoBehaviour
    {
        public bool destroyOnCollision;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (destroyOnCollision)
            {
                Destroy(gameObject);
            }
        }
    }
}
