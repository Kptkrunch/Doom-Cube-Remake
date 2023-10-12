using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Splines;
using Weapons.DeathRays;

public class DeathBlade : DeathRay
{
    public SplineAnimate splinePlayer;
    
    private void Update()
    {
        ResolveDeathRay();
    }

    public override void FireBeam()
    {
        lineRenderer.gameObject.SetActive(true);
        splinePlayer.gameObject.SetActive(true);
        splinePlayer.Play();
    }

    public override void ResolveDeathRay()
    {
        if (splinePlayer.IsPlaying)
        {
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, beamHitBox.transform.position);
        }

        if (!splinePlayer.IsPlaying)
        {
            lineRenderer.gameObject.SetActive(false);
            splinePlayer.Restart(false);
        }
    }
}
