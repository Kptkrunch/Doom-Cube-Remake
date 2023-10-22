using System.Collections.Generic;
using MoreMountains.Tools;
using UnityEngine;

public class MutantsAndBotsPoolManager : MonoBehaviour
{
    public static MutantsAndBotsPoolManager poolMutRob;
    public List<MMSimpleObjectPooler> mutAndRobPools;

    private void Awake()
    {
        poolMutRob = this;
    }
}
