using MoreMountains.Tools;
using UnityEngine;

namespace Objects
{
    public class SortObjectByYAxis : MonoBehaviour
    {
        public SpriteRenderer spriteRenderer;
        public Transform objectCenterTransform;
        public bool isStaticObject;
        
        private int _yAxis;

        private void Awake()
        {
            _yAxis = (int)(objectCenterTransform.position.y * -10).RoundDown(0);
            if (spriteRenderer)
            {
                spriteRenderer.sortingOrder = _yAxis;
            } 
        }

        private void FixedUpdate()
        {
            if (isStaticObject) return;
            _yAxis = (int)(objectCenterTransform.position.y * -10).RoundDown(0);
            spriteRenderer.sortingOrder = _yAxis;
        }
    }
}