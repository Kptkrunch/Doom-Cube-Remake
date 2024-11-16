using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using UnityEngine;

public class DeathrayHitParticles : MonoBehaviour
{
    public static DeathrayHitParticles Instance;
    public List<MMSimpleObjectPooler> hitParticlePoolers = new List<MMSimpleObjectPooler>();

    private void Awake()
    {
        Instance = this;
    }
}
