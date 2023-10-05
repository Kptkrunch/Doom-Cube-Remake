using JetBrains.Annotations;
using Objects;
using UnityEngine;

namespace GenUtilsAndTools
{
    public class WordlyObject : MonoBehaviour
    {
        public SpriteRenderer objRenderer;
        [CanBeNull] public Obstacle objParent;
        [CanBeNull] public Rigidbody2D objBody;
        [CanBeNull] public Sprite objSprite, stage1Sprite, stage2Sprite, stage3Sprite, destroyedSprite;
        public float currentDurability, maxDurability;
    
        private float _stage1dmg = .80f, _stage2dmg = .50f, _stage3dmg = .20f;

        private void Start()
        {
            currentDurability = maxDurability;
        }
        private void Update()
        {
            if (currentDurability.Equals(maxDurability))
            {
                objRenderer.sprite = objSprite;
            } else if (currentDurability <= _stage1dmg * maxDurability 
                       && currentDurability > _stage2dmg * maxDurability
                       && stage1Sprite)
            {
                objRenderer.sprite = stage1Sprite;
            } else if (currentDurability <= _stage2dmg * maxDurability 
                       && currentDurability > _stage3dmg * maxDurability 
                       && stage2Sprite)
            {
                objRenderer.sprite = stage2Sprite;
            } else if (currentDurability <= _stage3dmg * maxDurability 
                       && currentDurability > 0
                       && stage3Sprite)
            {
                objRenderer.sprite = stage3Sprite;
            } else if (currentDurability <= 0
                       && destroyedSprite)
            {
                objRenderer.sprite = destroyedSprite;
            }
        }

        public void TakeDamage(float damage)
        {
            currentDurability -= damage;
            if (objParent)
            {
                objParent.currentHealth -= damage;
            }
        }
    }
}
