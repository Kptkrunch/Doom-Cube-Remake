using MoreMountains.Feedbacks;
using UnityEngine;
using System.Collections.Generic;

namespace Controllers
{
    public class GenericJuiceManager : MonoBehaviour
    {
        public enum FeedbackType
        {
            Firing,
            Hit,
            Death,
            Collect,
            Idle,
            Passive
        }

        // Dictionary to map FeedbackType to MMF_Player instances
        private Dictionary<FeedbackType, MMF_Player> _feedbacks;

        // Assign these via Inspector
        public MMF_Player firingFeedbacks;
        public MMF_Player hitFeedbacks;
        public MMF_Player deathFeedbacks;
        public MMF_Player collectFeedbacks;
        public MMF_Player idleFeedbacks;
        public MMF_Player passiveFeedbacks;

        private void Awake()
        {
            // Initialize the dictionary with feedback mappings
            _feedbacks = new Dictionary<FeedbackType, MMF_Player>
            {
                { FeedbackType.Firing, firingFeedbacks },
                { FeedbackType.Hit, hitFeedbacks },
                { FeedbackType.Death, deathFeedbacks },
                { FeedbackType.Collect, collectFeedbacks },
                { FeedbackType.Idle, idleFeedbacks },
                { FeedbackType.Passive, passiveFeedbacks }
            };
        }

        public void TriggerFeedback(FeedbackType type)
        {
            if (_feedbacks.TryGetValue(type, out MMF_Player player) && player != null)
            {
                player.PlayFeedbacks();
            }
        }

        public void StopFeedback(FeedbackType type)
        {
            if (_feedbacks.TryGetValue(type, out MMF_Player player) && player != null)
            {
                player.StopFeedbacks();
            }
        }
    }
}