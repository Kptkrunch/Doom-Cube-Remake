using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathrayHitMarker : MonoBehaviour
{
    public int particleIndex;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            var hitParticle = DeathrayHitParticles.Instance.hitParticlePoolers[particleIndex].GetPooledGameObject();
            hitParticle.transform.position = other.transform.position;
            hitParticle.SetActive(true);
        }
    }
}
