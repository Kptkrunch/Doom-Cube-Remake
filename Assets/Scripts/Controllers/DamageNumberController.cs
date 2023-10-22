using MoreMountains.Feedbacks;
using UnityEngine;

namespace Controllers
{
    public class DamageNumberController : MonoBehaviour
    {
        public static DamageNumberController contDmgText;
        public MMF_Player player;
        private void Awake()
        {
            contDmgText = this;
        }
    }
}
