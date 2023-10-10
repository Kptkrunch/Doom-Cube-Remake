using UnityEngine;
using UnityEngine.InputSystem;

namespace Weapons.DeathRays
{
    public class ADeathRay : DeathRay
    {
        private Gamepad _gamepad;
        private void Update()
        {
            _gamepad = Gamepad.current;
            FireBeam();
            ResolveDeathRay();
        }

        private void FireBeam()
        {
            if (_gamepad.rightTrigger.wasPressedThisFrame && _gamepad != null)
            {
                lineRenderer.gameObject.SetActive(true);
                beamStart.gameObject.SetActive(true);
                beamHitBox.gameObject.SetActive(true);
                Vector3 position = transform.position;
                lineRenderer.SetPosition(0, position);
                fireDRay?.PlayFeedbacks();
            }
        }

        private void ResolveDeathRay()
        {
            if (fireDRay.IsPlaying)
            {
                lineRenderer.SetPosition(0, beamStart.transform.position);
                lineRenderer.SetPosition(1, hitMarker.transform.position);
            }

            if (!fireDRay.IsPlaying)
            {
                hitMarker.transform.position = pointA.transform.position;
                lineRenderer.gameObject.SetActive(false);
                beamStart.gameObject.SetActive(false);
                beamHitBox.gameObject.SetActive(false);
            }
        }
    }
}
