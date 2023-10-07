using Objects;
using UnityEngine;

namespace Damagers
{
    public class ObjectDamager : MonoBehaviour
    {
        public float damage;

        protected virtual void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("WorldlyObject"))
            {
                collision.GetComponent<WordlyObject>().TakeDamage(damage);
            }
        }
    }
}
