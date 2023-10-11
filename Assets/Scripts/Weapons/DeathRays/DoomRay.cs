using MoreMountains.Feedbacks;
using UnityEngine;

namespace Weapons.DeathRays
{
    public class DoomRay : DeathRay
    {
        public GameObject beamStart, hitMarker, pointA;
        public MMF_Player player;


        public override void FireBeam() {

            lineRenderer.gameObject.SetActive(true);
            beamStart.gameObject.SetActive(true);
            beamHitBox.gameObject.SetActive(true);
            Vector3 position = transform.position;
            lineRenderer.SetPosition(0, position);
            player?.PlayFeedbacks();
        }
    
        public override void ResolveDeathRay()
        {
            if (player.IsPlaying)
            {
                lineRenderer.SetPosition(0, beamStart.transform.position);
                lineRenderer.SetPosition(1, hitMarker.transform.position);
            }

            if (!player.IsPlaying)
            {
                hitMarker.transform.position = pointA.transform.position;
                lineRenderer.gameObject.SetActive(false);
                beamStart.gameObject.SetActive(false);
                beamHitBox.gameObject.SetActive(false);
            }
        }
    }
}
