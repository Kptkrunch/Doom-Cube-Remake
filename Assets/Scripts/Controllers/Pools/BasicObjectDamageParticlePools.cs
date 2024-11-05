using System;
using System.Collections.Generic;
using MoreMountains.Tools;
using UnityEngine;

namespace Controllers.Pools
{
    public class BasicObjectDamageParticlePools : MonoBehaviour
    {
        public static BasicObjectDamageParticlePools Instance;

        public Dictionary<string, List<MMSimpleObjectPooler>> ParticlePoolMatrix = new();
        public List<MMSimpleObjectPooler> firePool = new();
        public List<MMSimpleObjectPooler> acidPool = new();
        public List<MMSimpleObjectPooler> dustPool = new();
        public List<MMSimpleObjectPooler> explosionPool = new();

        private void Start()
        {
            InitializeMatrix();
        }

        private void Awake()
        {
            Instance = this;
        }

        public GameObject GetPooledDamagedParticle(string damageType, int particleIndex)
        {
            var particle = ParticlePoolMatrix[damageType][particleIndex].GetPooledGameObject();
            return particle;
        }

        private void InitializeMatrix()
        {
            ParticlePoolMatrix.Add("Fire", firePool);
            ParticlePoolMatrix.Add("Acid", acidPool);
            ParticlePoolMatrix.Add("Dust", dustPool);
            ParticlePoolMatrix.Add("Explosion", explosionPool);
        }
    }
}
