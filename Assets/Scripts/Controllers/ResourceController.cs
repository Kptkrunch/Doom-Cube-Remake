using System;
using ScriptableObjs;
using UnityEngine;

namespace Controllers
{
    public class ResourceController : MonoBehaviour
    {
        public static ResourceController contRes;
        public SoResources resources;
        public int meat, metal, mineral, plastic, energy;

        private void Awake()
        {
            contRes = this;
        }
    }
    
    
}