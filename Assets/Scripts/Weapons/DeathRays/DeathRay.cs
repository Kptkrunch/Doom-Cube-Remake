using Controllers;
using UnityEngine;

namespace Weapons.DeathRays
{
    public class DeathRay : MonoBehaviour
    {
        public int drid;
        public LineRenderer lineRenderer;
        public GenericJuiceManager juiceManager;
        public CircleCollider2D beamHitBox;
        private Vector3 _velocity, _beamStart, _beamEnd;

        private void Update()
        {
            ResolveDeathRay();
        }

        public virtual void FireBeam()
        {
            // set everything to active
        }

        protected virtual void ResolveDeathRay()
        {
            // run all the deactivate and line renderer
            // stuff here while playing anim
        }
    }
}