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
        public List<MMSimpleObjectPooler> smokePool = new();
        public List<MMSimpleObjectPooler> deathrayPool = new();

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
            if (particleIndex >= ParticlePoolMatrix[damageType].Count)
                particleIndex = ParticlePoolMatrix[damageType].Count - 1;
            var particle = ParticlePoolMatrix[damageType][particleIndex].GetPooledGameObject();
            return particle;
        }

        private void InitializeMatrix()
        {
            ParticlePoolMatrix.Add("Fire", firePool);
            ParticlePoolMatrix.Add("Acid", acidPool);
            ParticlePoolMatrix.Add("Dust", dustPool);
            ParticlePoolMatrix.Add("Explosion", explosionPool);
            ParticlePoolMatrix.Add("Smoke", smokePool);
            ParticlePoolMatrix.Add("Deathray", deathrayPool);
        }
    }
}
