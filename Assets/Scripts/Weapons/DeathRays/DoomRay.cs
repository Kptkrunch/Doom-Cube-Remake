using MoreMountains.Feedbacks;
using UnityEngine;

namespace Weapons.DeathRays
{
    public class DoomRay : DeathRay
    {
        public GameObject beamStart, hitMarker, pointA;
        public MMF_Player player;


        // ReSharper disable Unity.PerformanceAnalysis
        public override void FireBeam()
        {
            lineRenderer.gameObject.SetActive(true);
            beamStart.gameObject.SetActive(true);
            beamHitBox.gameObject.SetActive(true);
            var position = transform.position;
            lineRenderer.SetPosition(0, position);
            player?.PlayFeedbacks();
            if (MusicManager.Instance.sfxDeathRays.FeedbacksList[drid].FeedbackPlaying) return;
            MusicManager.Instance.sfxDeathRays.FeedbacksList[drid].Play(transform.position);
        }

        public override void ResolveDeathRay()
        {
            switch (player.IsPlaying)
            {
                case true:
                    lineRenderer.SetPosition(0, beamStart.transform.position);
                    lineRenderer.SetPosition(1, hitMarker.transform.position);
                    break;
                case false:
                    hitMarker.transform.position = pointA.transform.position;
                    lineRenderer.gameObject.SetActive(false);
                    beamStart.gameObject.SetActive(false);
                    beamHitBox.gameObject.SetActive(false);
                    break;
            }
        }
    }
}