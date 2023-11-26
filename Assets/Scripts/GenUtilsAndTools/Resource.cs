using Controllers;
using Controllers.Pools;
using JetBrains.Annotations;
using UnityEngine;

namespace GenUtilsAndTools
{
    public class Resource : AttractableItem
    {
        [CanBeNull] public Sprite resourceSprite;
        public int value;
        [SerializeField] private bool meat, metal, mineral, plastic, energy;
        

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.CompareTag("Player")) return;
            if (collision.CompareTag("Player"))
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
                isAttracted = false;
                gameObject.SetActive(false);
            }
        }
    }
}
