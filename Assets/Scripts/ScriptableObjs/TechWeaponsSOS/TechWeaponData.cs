using UnityEngine;

namespace ScriptableObjs.TechWeaponsSOS
{
    [CreateAssetMenu(fileName = "TechWeaponData", menuName = "ScriptableObjects/TechWeaponData", order = 1)]
    public class TechWeaponData : ScriptableObject
    {
        public int pid;
        public string techWeaponName;
        public string techWeaponDescription;
        public string damageType;
        public float rotationSpeed;
        public float range;
        public float fireRate;
    }
}