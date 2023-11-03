using System;
using UnityEngine;

namespace ScriptableObjs
{
        [CreateAssetMenu(fileName = "SOResources", menuName = "ScriptableObjects/SOResources")]

        [Serializable] public class SoResources : ScriptableObject
        {
                public int metal;
                public int plastic;
                public int energy;
        }
}
