using System;
using UnityEngine;

public class EnemyBools : MonoBehaviour
{
    public bool gotDeathParticle;
    public bool deathRay;
    public bool melting;

    private void Awake()
    {
        deathRay = false;
    }

    private void OnEnable()
    {
        deathRay = false;
        melting = false;
    }
}
