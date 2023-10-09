using MoreMountains.Feedbacks;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Weapons.DeathRays
{
    public class DeathRay : MonoBehaviour
    {
        public LineRenderer lineRenderer;
        public CircleCollider2D beamHitBox;
        public GameObject beamStart, hitMarker, pointA;
        public MMF_Player fireDRay;
        private Vector3 _velocity, _beamStart, _beamEnd;
    }
}
