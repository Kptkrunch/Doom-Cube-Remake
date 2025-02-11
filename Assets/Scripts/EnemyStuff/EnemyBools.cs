using System;
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

        private void OnEnable()
        {
            ResetEnemyBools();
        }

        private void ResetEnemyBools() {
            DmgTypeDictionary["Deathray"] = false;
            DmgTypeDictionary["Acid"] = false;
            DmgTypeDictionary["Fire"] = false;
            DmgTypeDictionary["Solid"] = false;
            DmgTypeDictionary["Energy"] = false;
            DmgTypeDictionary["Mind"] = false;
            
            gotDeathParticle = false;
            alreadyDropped = false;
        }
    }
}