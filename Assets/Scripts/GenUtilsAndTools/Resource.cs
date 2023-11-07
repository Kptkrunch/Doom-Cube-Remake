using Controllers;
using Controllers.Pools;
using JetBrains.Annotations;
using UnityEngine;

namespace GenUtilsAndTools
{
    public class Resource : MonoBehaviour
    {
        [CanBeNull] public Sprite resourceSprite;
        public int value;
        [SerializeField] private bool meat, metal, mineral, plastic, energy;
        

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.CompareTag("Player") || collision.CompareTag("ItemAttractor")) return;
            if (collision.CompareTag("Player") && !collision.CompareTag("ItemAttractor") &&
                !collision.CompareTag("Projectile") && !collision.CompareTag("Enemy"))
            {
                var particle = PickupParticlePool.poolPickup.poolDark.GetPooledGameObject();
                particle.SetActive(true);
                particle.gameObject.transform.position = transform.position;
                if (meat) ResourceController.contRes.meat += value;
                if (metal) ResourceController.contRes.metal += value;
                if (mineral) ResourceController.contRes.mineral += value;
                if (plastic) ResourceController.contRes.plastic += value;
                if (energy) ResourceController.contRes.plastic += value;
                ResourceController.contRes.UpdateResources();
                gameObject.SetActive(false);
            }
        }
    }
}
