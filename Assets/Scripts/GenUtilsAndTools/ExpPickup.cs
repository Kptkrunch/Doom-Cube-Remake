using Controllers;
using UnityEngine;

namespace GenUtilsAndTools
{
    public class ExpPickup : MonoBehaviour
    {
        [SerializeField] private int expValue;
    
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player") && !collision.CompareTag("ItemAttractor"))
            {
                ExperienceController.contExp.GetExp(expValue);
                Destroy(this.gameObject.gameObject);
            }
        }
    }
}
