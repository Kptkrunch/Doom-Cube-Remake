using System.Collections.Generic;
using Controllers;
using GenUtilsAndTools;
using MoreMountains.Feedbacks;
using UnityEngine;
using UnityEngine.Serialization;

namespace Objects
{
    public class BasicObject : MonoBehaviour
    {
        private static readonly int GreyscaleBlend = Shader.PropertyToID("_GreyscaleBlend");

        // Base object values
        public float currentDurability, maxDurability;

        // Required component references
        public SpriteRenderer spriteRenderer;
        public Sprite defaultSprite, destroyedSprite;
        public ResDropper resDropper;
        public Rigidbody2D objRb2d;
        public BoxCollider2D objCollider;
        public List<Sprite> objSpritesList = new();
        public List<GameObject> particleSpawnPointsList = new();
        
        private bool _lootDropped;
        private float _percentDamaged;
        private int _currentDamagedTier, _totalDamagedTiers;

        private void Start()
        {
            CheckAndSetTotalDamagedTiers();
        }

        private void Awake()
        {
            ResetOrInitThisBasicObject();
        }

        private void CheckAndUpdateCurrentDamagedTier()
        {
            if (_totalDamagedTiers > 0)
            {
                switch (_percentDamaged)
                {
                    case >= 0.2f and < 0.4f:
                    {
                        if (_totalDamagedTiers < 1) return;
                        _currentDamagedTier = 1;
                        break;
                    }
                    case >= 0.4f and < 0.6f:
                    {
                        if (_totalDamagedTiers < 2) return;
                        _currentDamagedTier = 2;
                        break;
                    }
                    case >= 0.6f and < 0.8f:
                    {
                        if(_totalDamagedTiers < 3) return;
                        _currentDamagedTier = 3;
                        break;
                    }
                    case >= 0.8f and < 0.99f:
                    {
                        if (_totalDamagedTiers < 4) return;
                        _currentDamagedTier = 4;
                        break;
                    }
                }
            }
        }
        // only update the sprite to either the default sprite or the destroyed sprite

        private void CheckAndSetTotalDamagedTiers()
        {
            if (particleSpawnPointsList.Count == 0) return;
            _totalDamagedTiers = particleSpawnPointsList.Count;
        }

        private void UpdateShaderGreyscaleBlend()
        {
            if (spriteRenderer != null && _percentDamaged > 0f)
            {
                spriteRenderer.sharedMaterial.SetFloat(GreyscaleBlend, _percentDamaged);
            }
        }

        private void UpdateObjectSpriteAndParticles()
        {
            var index = _currentDamagedTier - 1;
            if (_currentDamagedTier > 0 && objSpritesList[index] &&
                particleSpawnPointsList[index])
            {
                spriteRenderer.sprite = objSpritesList[index];
                particleSpawnPointsList[index].SetActive(true);
            } 
        }

        // ReSharper disable Unity.PerformanceAnalysis
        public void TakeDamage(float damage, string damageType)
        {
            currentDurability -= damage;
            _percentDamaged = 1 - (currentDurability / maxDurability);
            
            ShowDamage(damage);
            CheckAndUpdateCurrentDamagedTier();
            UpdateObjectSpriteAndParticles();
            ObjectDestroyedAndLootDroppedCheck();
        }
        
        private void ShowDamage(float damage, float intensity = 1f)
        {
            var floatingText = ObjDmgNumController.ContObjDmgNum
                .player.GetFeedbackOfType<MMF_FloatingText>();
            var damageString = damage.ToString();
            floatingText.Value = damageString;
            
            if (objRb2d)
            {
                if (ObjDmgNumController.ContObjDmgNum != null)
                    ObjDmgNumController.ContObjDmgNum.player.PlayFeedbacks(transform.position);
            }
        }
        
        private void ObjectDestroyedAndLootDroppedCheck()
        {
            if (currentDurability <= 0)
            {
                if (!_lootDropped && resDropper)
                {
                    _lootDropped = true;
                    resDropper.DropResource();
                }
                objCollider.enabled = false;
                spriteRenderer.sprite = destroyedSprite;
            }
        }
        
        private void ResetOrInitThisBasicObject()
        {
            currentDurability = maxDurability;
            _percentDamaged = 0f;
            _currentDamagedTier = 0;
            _lootDropped = false;
            objCollider.enabled = true;
            spriteRenderer.sprite = defaultSprite;
            UpdateShaderGreyscaleBlend();
            CheckAndSetTotalDamagedTiers();
        }
    }
}

