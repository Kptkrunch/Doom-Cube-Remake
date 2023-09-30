using Controllers;
using UnityEngine;

namespace EffectsTools
{
    public class ExpPickup : MonoBehaviour
    {
        public int expValue;
    
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                ExperienceController.expController.GetExp(expValue);
                Destroy(gameObject);
            }
        }
    }
}
