using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using UnityEngine;

public class ProjectileHitObjectPools : MonoBehaviour
{
    public static ProjectileHitObjectPools Instance;
    public List<MMSimpleObjectPooler> hitPools = new();
    
    
    private void Awake()
    {
        Instance = this;
    }
}
