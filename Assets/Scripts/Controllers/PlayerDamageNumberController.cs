using MoreMountains.Feedbacks;
using UnityEngine;

namespace Controllers
{
    public class PlayerDamageNumberController : MonoBehaviour
    {
        public static PlayerDamageNumberController ContPlayerDmgNum;
        public MMF_Player player;

        private void Awake()
        {
            ContPlayerDmgNum = this;
        }
    }
}
