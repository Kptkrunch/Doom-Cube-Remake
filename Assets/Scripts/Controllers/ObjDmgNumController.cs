using MoreMountains.Feedbacks;
using UnityEngine;

namespace Controllers
{
    public class ObjDmgNumController : MonoBehaviour
    {
        public static ObjDmgNumController ContObjDmgNum;
        public MMF_Player player;

        private void Awake()
        {
            ContObjDmgNum = this;
        }
    }
}