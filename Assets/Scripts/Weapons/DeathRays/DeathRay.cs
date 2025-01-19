using Controllers;
using UnityEngine;

namespace Weapons.DeathRays
{
    public class DeathRay : MonoBehaviour
    {
        public int drid;
        public LineRenderer lineRenderer;
        public CircleCollider2D beamHitBox;
        public GenericJuiceManager juiceManager;
        private Vector3 _velocity, _beamStart, _beamEnd;
        

        private void Update()
        {
            ResolveDeathRay();
        }

        public virtual void FireBeam()
        {
            // set everything to active
            juiceManager.TriggerFeedback(GenericJuiceManager.FeedbackType.Firing);
        }

        public virtual void ResolveDeathRay()
        {
            // run all the deactivate and line renderer
            // stuff here while playing anim
        }
    }
}