using System;
using UnityEngine;

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
        public bool hasLifetime;
        public bool useTranslate;
        public bool disableAfterBounces;
        public bool disableOnContact;
        public bool randomDirection;
        public bool movesBackwards;
        public bool moveProjectile;
    }
}
