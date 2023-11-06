using System;
using UnityEngine;

namespace Weapons.Projectiles
{
    [CreateAssetMenu(fileName = "New Projectile", menuName = "Projectile")]
    public class ProjectileData : ScriptableObject
    {
        public string projName;
        public int pid;
        public int eid;
        public ProjStats stats = new();
        
        [Serializable]
        public class ProjStats
        {
            public float movSpeed;
            public float damage;
            public float size;
            public float lobHeight;
            public float lobDistance;
            public float rotSpeed;
            public float lifeTime;
            public float pens;
            public float bounces;
            public float bounceInterval;
            public Vector2 direction;
        }
    }
}