using System.Collections.Generic;
using Controllers;
using Controllers.Pools;
using GenUtilsAndTools;
using MoreMountains.Feedbacks;
using UnityEngine;

namespace Objects
{
    public class BasicObject : MonoBehaviour
    {
        private static readonly int GreyscaleBlend = Shader.PropertyToID("_GreyscaleBlend");

        // Base object values
        public float currentDurability, maxDurability;

        // Required component references
        public SpriteRenderer spriteRenderer;
        public ResDropper resDropper;
        public Rigidbody2D objRb2d;
        public BoxCollider2D objCollider;
        public List<Sprite> objSpritesList = new();
        public List<GameObject> particleSpawnPointsList = new();
        
        private Sprite _defaultSprite, _destroyedSprite;
        private bool _lootDropped, _wasInitialized;
        private float _percentDamaged;
        private int _currentDamagedTier, _totalDamagedTiers;

        private void Start()
        {
            if (!_wasInitialized) CheckAndSetTotalDamagedTiers();
        }

        private void Awake()
        {
            ResetOrInitThisBasicObject();
            if (_wasInitialized) UpdateBasicObjectVisuals();
        }



        // Must have a particle spawn location for each damage tier
        private void SpawnDamageTierParticle(int damageTier, string damageType)
        {
            var particleIndex = Random.Range(0, 3);
            if (damageTier > _totalDamagedTiers) return;

            if (damageTier <= particleSpawnPointsList.Count)
            {
                var particle = BasicObjectDamageParticlePools.Instance.GetPooledDamagedParticle(damageType, particleIndex);
                particle.SetActive(true);
                particle.transform.position = particleSpawnPointsList[damageTier].transform.position;
            }
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
        private void UpdateDefaultOrDestroyedSprite()
        {
            // update the default or destroyed sprite
            if (currentDurability >= maxDurability&& _defaultSprite)
            {
                currentDurability = maxDurability;
                spriteRenderer.sprite = _defaultSprite;
            } else if (currentDurability <= 0f && _destroyedSprite)
            {
                currentDurability = 0f;
                spriteRenderer.sprite = _destroyedSprite;
            }
        }
        // Update starting and destroyed sprites, and update sprite if it has damage tiers
        private void UpdateBasicObjectVisuals()
        {
            
            CheckAndUpdateCurrentDamagedTier();
            UpdateDamagedTierSprite();
            UpdateShaderGreyscaleBlend();
        }

        private void CheckAndSetTotalDamagedTiers()
        {
            if (particleSpawnPointsList != null && particleSpawnPointsList.Count > 0)
            {
                _totalDamagedTiers = particleSpawnPointsList.Count;
            }
        }

        private void UpdateShaderGreyscaleBlend()
        {
            if (spriteRenderer != null && _percentDamaged > 0f)
            {
                spriteRenderer.sharedMaterial.SetFloat(GreyscaleBlend, _percentDamaged);
            }
        }

        private void UpdateDamagedTierSprite()
        {
            if (spriteRenderer && objSpritesList[_currentDamagedTier])
            {
                spriteRenderer.sprite = objSpritesList[_currentDamagedTier];
            }
        }

        // ReSharper disable Unity.PerformanceAnalysis
        public void TakeDamage(float damage, string damageType)
        {
            currentDurability -= damage;
            _percentDamaged = 1 - (currentDurability / maxDurability);
            
            ShowDamage(damage);
            CheckAndUpdateCurrentDamagedTier();
            if (_currentDamagedTier > 0 && currentDurability > 0)
            {
                SpawnDamageTierParticle(_currentDamagedTier, damageType);
            }
            UpdateBasicObjectVisuals();
            
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
                UpdateDefaultOrDestroyedSprite();
            }
        }
        private void ResetOrInitThisBasicObject()
        {
            currentDurability = maxDurability;
            _percentDamaged = 0f;
            _currentDamagedTier = 0;
            _lootDropped = false;
            objCollider.enabled = true;

            if (objSpritesList == null) return;
            
            _defaultSprite = objSpritesList[0];
            spriteRenderer.sprite = _defaultSprite;
            
            var finalIndex = objSpritesList.Count - 1;
            _destroyedSprite = objSpritesList[finalIndex];
        
            _wasInitialized = true;
        }
    }
}

