using UnityEngine;

namespace Controllers
{
    public class PlayerStatsController : MonoBehaviour
    {
        public static PlayerStatsController contStats;
        public float moveSpeed, maxWeapons, maxHealth;

        private void Awake()
        {
            contStats = this;
        }
    }
}