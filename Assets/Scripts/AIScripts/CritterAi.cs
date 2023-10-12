using JetBrains.Annotations;
using UnityEngine;

namespace AIScripts
{
    public class CritterAi : MonoBehaviour
    {
        [CanBeNull] public CircleCollider2D aggroArea;
        [CanBeNull] public Follow follow;
        public float aggroRadius, attackRange;
        public bool enemies, obstacles, civilians, vehicles, catchingUp;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Enemy"))
            {
                Vector3.MoveTowards(transform.position, collision.transform.position, aggroRadius);
            }
        }
    
    
    }
}
