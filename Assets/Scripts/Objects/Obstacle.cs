using System.Collections.Generic;
using Controllers;
using GenUtilsAndTools;
using JetBrains.Annotations;
using UnityEngine;

namespace Objects
{
    public class Obstacle : MonoBehaviour
    {
        public float currentHealth;
        public bool isDestroyed, isExplosive, isBurning;
        [CanBeNull] public List<WordlyObject> objects;
        
        
        [CanBeNull] private GameObject _dmgParticle1, _dmgParticle2, _dmgParticle3, _explode1, _wreckage;
        [SerializeField] private float maxHealth, stage1dmg = 0.75f, stage2dmg = 0.35f, stage3dmg = .10f;


        private void Start()
        {
            GetParticles();
            GetStructuralIntegrity();
        }
        private void Update()
        {
            if (currentHealth < maxHealth * stage1dmg
                && currentHealth > maxHealth * stage2dmg)
            {
                if (_dmgParticle1)
                {
                    _dmgParticle1.SetActive(true);
                    _dmgParticle1.transform.position = transform.position;
                }
            }

            if (currentHealth <= maxHealth * stage2dmg
                && currentHealth > maxHealth * stage3dmg)
            {
                if (_dmgParticle2)
                {
                    if (_dmgParticle2)
                    {
                        _dmgParticle2.SetActive(true);
                        _dmgParticle2.transform.position = transform.position;
                    }
                    
                }
            }

            if (currentHealth <= maxHealth * stage3dmg
                && currentHealth > 0)
            {
                if (_dmgParticle3)
                {
                    if (_dmgParticle3)
                    {
                        _dmgParticle3.SetActive(true);
                        _dmgParticle3.transform.position = transform.position;
                    }
                }
            }

            if (currentHealth <= 0)
            {
                if (_wreckage)
                {
                    _wreckage.SetActive(true);
                }
            }

            if (isDestroyed)
            {
                gameObject.SetActive(false);
                if (isExplosive && _explode1)
                {
                    _explode1.SetActive(true);
                }
            }
        }

        private void GetStructuralIntegrity()
        {
            if (objects != null)
                for (var i = 0; i < objects.Count; i++)
                {
                    maxHealth += objects[i].maxDurability;
                }

            currentHealth = maxHealth;
        }

        private void GetParticles()
        {
            var mmSimpleObjectPooler = PoolController.contPool.GetComponent<DustController>();
            if (mmSimpleObjectPooler.sDust)
            {
                _dmgParticle1 = mmSimpleObjectPooler.sDust.GetPooledGameObject();
            }
            
            if (mmSimpleObjectPooler.mDust)
            {
                _dmgParticle2 = mmSimpleObjectPooler.mDust.GetPooledGameObject();
            }

            if (mmSimpleObjectPooler.bDust)
            {
                _dmgParticle3 = mmSimpleObjectPooler.bDust.GetPooledGameObject();
            }
        }
    }
}
