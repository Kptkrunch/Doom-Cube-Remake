using System;
using UnityEngine;

namespace ScriptableObjs
{
        [CreateAssetMenu(fileName = "SOResources", menuName = "ScriptableObjects/SOResources")]

        [Serializable] public class SoResources : ScriptableObject
        {
                public int meat;
                public int metal;
                public int mineral;
                public int plastic;
                public int energy;
        }
}
