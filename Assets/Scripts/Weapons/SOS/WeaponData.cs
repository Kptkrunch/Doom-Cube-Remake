using System;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{
    [CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon")]
    public class WeaponData : ScriptableObject
    {
        public string weaponName;
        public int lvl;
        public int wid;
        public int pid;
        public GameObject weaponPrefab;
        
        [System.Serializable]
        public class WeaponLevel
        {
            public float damage;
            public float ammo;
            public float range;
            public float coolDown;
            public float rateOfFire;
            public float duration;
            public float speed;
            public Vector3 size;
            public float wildCard;
            public string upgradeText;
        }

        public List<WeaponLevel> weaponLvls;
    }
}