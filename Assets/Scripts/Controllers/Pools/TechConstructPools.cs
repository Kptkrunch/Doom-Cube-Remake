using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using UnityEngine;

public class TechConstructPools : MonoBehaviour
{
    public static TechConstructPools ConstructPools;
    public List<MMSimpleObjectPooler> constructPoolList = new();

    private void Awake()
    {
        ConstructPools = this;
    }

    public GameObject GetConstructById(int constructId)
    {
        var construct = constructPoolList[constructId].GetPooledGameObject();
        return construct;
    }
}
