using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GenUtilsAndTools
{
    public class EnemyBools : MonoBehaviour
    {
        public bool gotDeathParticle, isMelee, isRanged;
        [ShowInInspector]
        public Dictionary<string, bool> DmgTypeDictionary = new()
        {
            {"deathray", false},
            {"melting", false},
            {"burning", false},
            {"physical", false},
            {"energy", false},
            {"mental", false},
        };
    }
}

