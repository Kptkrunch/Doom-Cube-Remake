using MoreMountains.Feedbacks;
using UnityEngine;

namespace Controllers
{
    public class TechDamageNumberController : MonoBehaviour
    {
        public static TechDamageNumberController ContTechDmgNum;
        public MMF_Player player;

        private void Awake()
        {
            ContTechDmgNum = this;
        }
    }
}