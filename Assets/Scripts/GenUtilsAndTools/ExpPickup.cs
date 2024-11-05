using Controllers;
using UnityEngine;

namespace GenUtilsAndTools
{
    public class ExpPickup : AttractableItem
    {
        [SerializeField] private int expValue;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.CompareTag("Player")) return;
            if (collision.CompareTag("Player"))
            {
                ExperienceController.contExp.GetExp(expValue);
                isAttracted = false;
                gameObject.SetActive(false);
            }
        }
    }
}