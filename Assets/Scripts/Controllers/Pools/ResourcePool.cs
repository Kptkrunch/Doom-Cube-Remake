using System;
using System.Collections;
using System.Collections.Generic;
using Controllers;
using MoreMountains.Tools;
using UnityEngine;

public class ResourcePool : MonoBehaviour
{
    public static ResourcePool poolRes;
    public MMSimpleObjectPooler poolMeat, poolMetal, poolMineral, poolPlastic, poolEnergy;

    private void Awake()
    {
        poolRes = this;
    }
}
