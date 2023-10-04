using Controllers;
using UnityEngine;

namespace GenUtilsAndTools
{
    public class ObjectDamager : MonoBehaviour
    {
        public float damage;

        protected virtual void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Object"))
            {
                collision.GetComponent<WordlyObject>().TakeDamage(damage);
            }
        }
    }
}
