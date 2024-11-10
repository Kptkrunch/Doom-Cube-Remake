using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace EnemyStuff
{
    public class EnemyBools : MonoBehaviour
    {
        public bool gotDeathParticle, alreadyDropped;

        [ShowInInspector] public Dictionary<string, bool> DmgTypeDictionary = new()
        {
            { "Deathray", false },
            { "Acid", false },
            { "Fire", false },
            { "Solid", false },
            { "Energy", false },
            { "Mind", false }
        };
    }
}