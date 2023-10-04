using JetBrains.Annotations;
using UnityEngine;

public class WordlyObject : MonoBehaviour
{
    public SpriteRenderer objRenderer;
    [CanBeNull] public GameObject objParent;
    [CanBeNull] public Rigidbody2D objBody;
    [CanBeNull] public Sprite objSprite, stage1Sprite, stage2Sprite, stage3Sprite, destroyedSprite;
    public float currentDurability, maxDurability;
    [CanBeNull] public GameObject fireDamage, smoke, dustAndDebris, explosion;
    
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
        } else if (currentDurability <= _stage1dmg 
                   && currentDurability > _stage2dmg
                   && stage1Sprite)
        {
            objRenderer.sprite = stage1Sprite;
        } else if (currentDurability <= _stage2dmg
                   && currentDurability > _stage3dmg
                   && stage2Sprite)
        {
            objRenderer.sprite = stage2Sprite;
        } else if (currentDurability <= _stage3dmg
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
    }
}
