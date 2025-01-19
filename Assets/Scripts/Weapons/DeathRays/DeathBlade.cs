using UnityEngine;
using UnityEngine.Splines;

namespace Weapons.DeathRays
{
    public class DeathBlade : DeathRay
    {
        public SplineAnimate splinePlayer;
        public GameObject beamOrigin;
        public GameObject hitMarker;

        private void Update()
        {
            ResolveDeathRay();
        }

        public override void FireBeam()
        {
            lineRenderer.gameObject.SetActive(true);
            splinePlayer.gameObject.SetActive(true);
            hitMarker.SetActive(true);
            splinePlayer.Play();
            DeathRaySfxGroupController.Instance.sfxControllers[drid].player.FeedbacksList[0].Play(transform.position);
        }

        protected override void ResolveDeathRay()
        {
            if (splinePlayer.IsPlaying)
            {
                beamOrigin.SetActive(true);
                lineRenderer.SetPosition(0, transform.position);
                lineRenderer.SetPosition(1, beamHitBox.transform.position);
            }

            if (!splinePlayer.IsPlaying)
            {
                beamOrigin.SetActive(false);
                lineRenderer.gameObject.SetActive(false);
                hitMarker.SetActive(false);
                splinePlayer.Restart(false);
            }

            if (!hitMarker.activeSelf
                && DeathRaySfxGroupController.Instance.sfxControllers[drid].player.FeedbacksList[0].IsPlaying)
            {
                DeathRaySfxGroupController.Instance.sfxControllers[drid].player.FeedbacksList[0].Stop(transform.position);
            }
        }
    }
}