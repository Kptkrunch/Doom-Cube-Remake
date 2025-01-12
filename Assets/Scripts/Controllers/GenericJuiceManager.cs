using MoreMountains.Feedbacks;
using UnityEngine;

namespace Controllers
{
    public class GenericJuiceManager : MonoBehaviour
    {

        public bool doesFire, doesHit, doesDie;
        public MMF_Player firingFeedbacks, hitFeedbacks, deathFeedbacks, collectFeedbacks;

        public void TriggerFiringFeedback()
        {
            if (firingFeedbacks != null)
            {
                firingFeedbacks.PlayFeedbacks();
            }
        }
        
        public void TriggerHitFeedback()
        {
            if (hitFeedbacks != null)
            {
                hitFeedbacks.PlayFeedbacks();
            }
        }

        public void TriggerDeathFeedback()
        {
            if (deathFeedbacks != null)
            {
                deathFeedbacks.PlayFeedbacks();
            }
        }
        
        public void TriggerCollectFeedback()
        {
            if (collectFeedbacks != null)
            {
                collectFeedbacks.PlayFeedbacks();
            }
        }
    }
}