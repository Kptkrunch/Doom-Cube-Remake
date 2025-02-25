using MoreMountains.Feedbacks;
using UnityEngine;

namespace Controllers
{
    public class DamageNumberController : MonoBehaviour
    {
        public static DamageNumberController Instance;
        public MMF_Player player;

        private void Awake()
        {
            Instance = this;
        }
    }
}