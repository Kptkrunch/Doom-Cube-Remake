using UnityEngine;
using Random = UnityEngine.Random;

namespace EffectsTools
{
    public class EnemyAnimation : MonoBehaviour
    {
        public Transform sprite;
        public float speed;
        public float minSize, maxSize;
        private float _activeSize;
        void Start()
        {
            _activeSize = maxSize;
        
            speed = speed * Random.Range(.75f, 1.25f);
        }
    
        void Update()
        {
            sprite.localScale = Vector3.MoveTowards(sprite.localScale, Vector3.one * _activeSize, speed * Time.deltaTime);
            if (sprite.localScale.x == _activeSize)
            {
                if (_activeSize == maxSize)
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
