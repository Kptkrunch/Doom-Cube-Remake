using UnityEngine;
using Random = UnityEngine.Random;

namespace GenUtilsAndTools
{
    public class EnemyAnimation : MonoBehaviour
    {
        public Transform sprite;
        public float speed,minSize, maxSize;
        
        private float _activeSize;
        private void Start()
        {
            _activeSize = maxSize;
        
            speed *= Random.Range(.75f, 1.25f);
        }
    
        private void Update()
        {
            sprite.localScale = Vector3.MoveTowards(sprite.localScale, Vector3.one * _activeSize, speed * Time.deltaTime);
            if (sprite.localScale.x.Equals(_activeSize))
            {
                if (_activeSize.Equals(maxSize))
                {
                    _activeSize = minSize;
                }
                else
                {
                    _activeSize = maxSize;
                }
            }
        }
    }
}
