using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Weapons.WeaponModifiers
{
    [Serializable]
    public class BehaviorChecklist : MonoBehaviour
    {
        public bool delayDisable;
        public bool doesPenetrate;
        public bool doesRotate;
        public bool isLobbed;
        public bool doesBounce;
        public bool itExplodes;
        public bool hasLifetime;
        public bool disableAfterBounces;
        public bool disableOnContact;
        public bool randomDirection;
        public bool movesBackwards;
        public bool moveUseTranslate;
        public bool moveUseVelocity;
        public bool moveUseMoveTowards;
    }
}
