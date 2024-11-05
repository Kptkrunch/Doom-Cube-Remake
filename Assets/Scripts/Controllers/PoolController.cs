using UnityEngine;

namespace Controllers
{
    public class PoolController : MonoBehaviour
    {
        public static PoolController contPool;

        private void Awake()
        {
            contPool = this;
        }
    }
}