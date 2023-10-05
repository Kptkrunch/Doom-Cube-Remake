using MoreMountains.Feedbacks;
using UnityEngine;

public class DeathRay : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public CircleCollider2D beamHitBox;
    public GameObject beamStart, hitMarker, pointA;
    public MMF_Player fireDRay;
    private Vector3 _velocity, _beamStart, _beamEnd;

    private void Update()
    {
        if (Input.GetButtonDown("Fire1")) FireBeam();
        ResolveDeathRay();
    }

    private void FireBeam()
    {
        lineRenderer.gameObject.SetActive(true);
        beamHitBox.gameObject.SetActive(true);
        Vector3 position = transform.position;
        lineRenderer.SetPosition(0, position);
        fireDRay?.PlayFeedbacks();
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
            beamHitBox.gameObject.SetActive(false);
        }
    }
}
