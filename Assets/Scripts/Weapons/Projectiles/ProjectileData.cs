using UnityEngine;

namespace Weapons.Projectiles
{
    [CreateAssetMenu(fileName = "New Projectile", menuName = "Projectile")]
    public class ProjectileData : ScriptableObject
    {
        public float speed;
        public float damage;
        public float size;
        public GameObject projectilePrefab;
    }
}