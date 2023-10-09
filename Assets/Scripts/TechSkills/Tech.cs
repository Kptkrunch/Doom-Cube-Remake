using System.Collections.Generic;
using Controllers;
using UnityEngine;

namespace TechSkills
{
    public class Tech : MonoBehaviour
    {
        public int techLevel;
        public Sprite techImage;
        public int costMeat, costMetal, costMineral, costPlastic, costEnergy;
        public List<TechStats> stats = new();
        
        public virtual void ActivateTech(string button)
        {
            switch (button)
            {
                case "a":

                    break;
                case "x":

                    break;
                case "y":

                    break;
                case "b":

                    break;
            }
        }
        
            
        public void UpdateTech()
        {
        
        }
    }
}

[System.Serializable]
public class TechStats
{
    public Sprite techSprite;
    public string description;
    public float actMeat, actMetal, actMineral, actPlastic, actEnergy;
    public float duration, damage, range, maxHealth;
}
