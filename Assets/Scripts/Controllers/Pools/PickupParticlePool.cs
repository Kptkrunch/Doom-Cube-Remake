using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using UnityEngine;

public class PickupParticlePool : MonoBehaviour
{
    public static PickupParticlePool poolPickup;
    public MMSimpleObjectPooler poolDark, poolRed;
    
    private void Awake()
    {
        poolPickup = this;
    }
}
