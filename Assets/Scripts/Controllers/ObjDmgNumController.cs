using MoreMountains.Feedbacks;
using UnityEngine;

namespace Controllers
{
    public class ObjDmgNumController : MonoBehaviour
    {
        public static ObjDmgNumController contObjDmgNum;
        public MMF_Player player;
        private void Awake()
        {
            contObjDmgNum = this;
        }
    }
}
