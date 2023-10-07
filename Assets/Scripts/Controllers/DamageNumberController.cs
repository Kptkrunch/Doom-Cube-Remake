using System.Collections.Generic;
using MoreMountains.Feedbacks;
using UI;
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
