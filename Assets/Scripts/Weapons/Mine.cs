using Damagers;
using UnityEngine;

public class Mine : MonoBehaviour
{
    public EExplosionDamager explosion;
    public GameObject parent;
    public bool hasLifetime, hasFuse;
    public float fuseTimer = 3;
    public float lifeTimer = 10;

    private void FixedUpdate()
    {
        if (hasLifetime) lifeTimer -= Time.deltaTime;
        if (hasFuse) fuseTimer -= Time.deltaTime;
        
        if (hasLifetime && lifeTimer <= 0)
        {
            gameObject.SetActive(false);
        } else if (hasFuse && fuseTimer <= 0)
        {
            explosion.Detonate();
            parent.gameObject.SetActive(false);
        }
    }
}
