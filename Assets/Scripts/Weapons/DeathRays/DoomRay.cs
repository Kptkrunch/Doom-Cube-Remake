using Controllers;
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
            DeathRaySfxGroupController.Instance.sfxControllers[drid].player.FeedbacksList[0].Play(transform.position);
        }

        protected override void ResolveDeathRay()
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
                    DeathRaySfxGroupController.Instance.sfxControllers[drid].player.FeedbacksList[0].Stop(transform.position);
                    player?.StopFeedbacks();
                    break;
            }
        }
    }
}