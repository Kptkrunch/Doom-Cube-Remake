using UnityEngine;

namespace GenUtilsAndTools
{
    public class ItemAttractor : MonoBehaviour
    {
        public static ItemAttractor itemAttractor;
        public float pickupRadius;
        public float pullSpeed;
        public CircleCollider2D pickupArea;
        private void Awake()
        {
            itemAttractor = this;
            pickupArea.radius = pickupRadius;
        }
    }
}
