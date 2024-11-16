using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using UnityEngine;

public class MuzzleFlashPools : MonoBehaviour
{
    public static MuzzleFlashPools Instance;
    public List<MMSimpleObjectPooler> flashPools = new();
    
    
    private void Awake()
    {
        Instance = this;
    }
}
