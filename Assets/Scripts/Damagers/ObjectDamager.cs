using Objects;
using UnityEngine;

namespace Damagers
{
    public sealed class ObjectDamager : MonoBehaviour
    {
        public float damage;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("WorldlyObject"))
            {
                collision.GetComponent<WordlyObject>().TakeDamage(damage);
            }
        }
    }
}
