using MoreMountains.Feedbacks;
using UnityEngine;

namespace Controllers
{
    public class GenericShakeController : MonoBehaviour
    {
        public static GenericShakeController Instance;
        
        public MMF_Player weakPlayer, strongPlayer, violentPlayer;
        private void Awake()
        {
            Instance = this;
        }
        
        public void ShakeWeakStrongViolent(string shakeStrength, Transform shakeOrigin)
        {
            switch (shakeStrength)
            {
                case "weak": 
                    weakPlayer.FeedbacksList[0].Play(shakeOrigin.position);
                    break;
                case "strong":
                    strongPlayer.FeedbacksList[0].Play(shakeOrigin.position);
                    break;
                case "violent":
                    violentPlayer.FeedbacksList[0].Play(shakeOrigin.position);
                    break;
            }
        }
    }
}
