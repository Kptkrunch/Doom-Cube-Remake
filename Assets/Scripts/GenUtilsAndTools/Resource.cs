using Controllers;
using JetBrains.Annotations;
using UnityEngine;

namespace GenUtilsAndTools
{
    public class Resource : MonoBehaviour
    {
        [CanBeNull] public Sprite resourceSprite;
        [CanBeNull] public GameObject pickupEffect;
        public int value;
        public bool meat, metal, mineral, plastic, energy;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("ItemAttractor"))
            {
                if (meat) ResourceController.contRes.meat += value;
                if (metal) ResourceController.contRes.metal += value;
                if (mineral) ResourceController.contRes.mineral += value;
                if (plastic) ResourceController.contRes.plastic += value;
                if (energy) ResourceController.contRes.plastic += value;
            }
            if (pickupEffect != null) pickupEffect.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
