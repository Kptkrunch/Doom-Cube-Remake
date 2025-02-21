using MoreMountains.Tools;
using UnityEngine;

namespace Weapons.Projectiles
{
    public class Transmogrifier : MonoBehaviour
    {
        public MMSimpleObjectPooler replacementPool;
        protected virtual void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.CompareTag("Enemy")) return;
            Transmogrify(collision);
        }
        
        private void Transmogrify(Collider2D collision)
        {
            if (collision) collision.gameObject.SetActive(false);
            var targetPosition = collision.transform.position;
            var replacementObject = replacementPool.GetPooledGameObject();
            replacementObject.SetActive(true);
            replacementObject.transform.position = targetPosition;
            gameObject.SetActive(false);
        }
    }
}
