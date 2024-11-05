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
        // Base object values
        public float currentDurability, maxDurability;

        // Required component references
        public SpriteRenderer spriteRenderer;
        public ResDropper resDropper;
        public Rigidbody2D objRb2d;
        public BoxCollider2D objCollider;
        public List<Sprite> objSpritesList = new();
        public List<GameObject> particleSpawnPointsList = new();
        
        
        public string shaderEffectNameString = "_GreyscaleBlend";

        private Sprite _defaultSprite, _destroyedSprite;
        private bool _lootDropped;
        private float _percentDamaged;
        private int _currentDamagedTier = 0, _totalDamagedTiers = 0;

        private void Start()
        {
            // Set the default and destroyed sprites
            if (objSpritesList.Count >= 2)
            {
                var finalIndex = objSpritesList.Count - 1;
                _defaultSprite = objSpritesList[0];
                _destroyedSprite = objSpritesList[finalIndex];
            }

            if (objSpritesList.Count > 2)
            {
                _totalDamagedTiers = objSpritesList.Count - 2;
            }
        }

        private void Awake()
        {
            ResetOrInitThisBasicObject();
        }

        // Update starting and destroyed sprites, and update sprite if it has damage tiers
        private void UpdateSpriteAndShader()
        {
            // update the default or destroyed sprite
            if (currentDurability >= maxDurability&& _defaultSprite)
            {
                if (currentDurability > maxDurability) currentDurability = maxDurability;
                spriteRenderer.sprite = _defaultSprite;
            } else if (currentDurability <= 0f && _destroyedSprite)
            {
                if (currentDurability <= 0) currentDurability = 0f;
                spriteRenderer.sprite = _defaultSprite;
            }

            // If there are 3 or more Sprites it will have damage tiers/stages
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
            
            // Update all in one shader
            if (spriteRenderer && _percentDamaged > 0f)
            {
                spriteRenderer.sharedMaterial.SetFloat(shaderEffectNameString, _percentDamaged);
            }
        }

        // Must have a particle spawn location for each damage tier
        private void SpawnDamageTierParticle(int damageTier, string damageType)
        {
            var particleIndex = Random.Range(0, 3);
            if (damageTier > _totalDamagedTiers) return;

            if (damageTier < particleSpawnPointsList.Count)
            {
                var particle = BasicObjectDamageParticlePools.Instance.GetPooledDamagedParticle(damageType, particleIndex);
                particle.SetActive(true);
                particle.transform.position = particleSpawnPointsList[damageTier].transform.position;
            }

        }

        // ReSharper disable Unity.PerformanceAnalysis
        public void TakeDamage(float damage, string damageType)
        {
            currentDurability -= damage;
            _percentDamaged = 1 - (currentDurability / maxDurability);
            ShowDamage(damage);
            UpdateSpriteAndShader();
            if (_currentDamagedTier > 0) SpawnDamageTierParticle(_currentDamagedTier, damageType);
            
            if (currentDurability <= 0)
            {
                if (!_lootDropped)
                {
                    _lootDropped = true;
                    resDropper.DropResource();
                }

                if (_destroyedSprite != null)
                {
                    UpdateSpriteAndShader();
                }
            }
        }
        private void ShowDamage(float damage, float intensity = 1f)
        {
            var floatingText = ObjDmgNumController.ContObjDmgNum
                .player.GetFeedbackOfType<MMF_FloatingText>();
            floatingText.Value = damage.ToString();
            
            if (objRb2d)
            {
                if (ObjDmgNumController.ContObjDmgNum != null)
                    ObjDmgNumController.ContObjDmgNum.player.PlayFeedbacks(transform.position);
            }
        }
        private void ResetOrInitThisBasicObject()
        {
            spriteRenderer.sprite = _defaultSprite;
            currentDurability = maxDurability;
            _percentDamaged = 0f;
            _currentDamagedTier = 0;
            _lootDropped = false;
            objCollider.enabled = true;
        }
    }
}

