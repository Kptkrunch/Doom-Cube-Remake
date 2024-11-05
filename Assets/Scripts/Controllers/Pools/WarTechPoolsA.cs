using MoreMountains.Tools;
using UnityEngine;

public class WarTechPoolsA : MonoBehaviour
{
    public static WarTechPoolsA poolsA;
    public MMSimpleObjectPooler tech1, tech2, tech3, tech4;

    private void Awake()
    {
        poolsA = this;
    }
}