using Controllers;
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
            juiceManager.TriggerFeedback(GenericJuiceManager.FeedbackType.Firing);
        }

        protected override void ResolveDeathRay()
        {
            switch (splinePlayer.IsPlaying)
            {
                case true:
                    beamOrigin.SetActive(true);
                    lineRenderer.SetPosition(0, transform.position);
                    lineRenderer.SetPosition(1, beamHitBox.transform.position);
                    break;
                case false:
                    beamOrigin.SetActive(false);
                    lineRenderer.gameObject.SetActive(false);
                    hitMarker.SetActive(false);
                    splinePlayer.Restart(false);
                    break;
            }

            if (!juiceManager.firingFeedbacks.IsPlaying)
            {
                juiceManager.StopFeedback(GenericJuiceManager.FeedbackType.Firing);
            }
        }
    }
}