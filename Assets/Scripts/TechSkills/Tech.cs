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
        private GameObject _obj;
        
        public virtual void ActivateTech(string button)
        {
            switch (button)
            {
                case "a":
                    _obj = WarTechPoolsA.poolsA.tech1.GetPooledGameObject();
                    _obj.SetActive(true);
                    _obj.transform.position = transform.position;
                    break;
                case "x":
                    _obj = WarTechPoolsA.poolsA.tech1.GetPooledGameObject();
                    _obj.SetActive(true);
                    _obj.transform.position = transform.position;
                    break;
                case "y":
                    _obj = WarTechPoolsA.poolsA.tech1.GetPooledGameObject();
                    _obj.SetActive(true);
                    _obj.transform.position = transform.position;
                    break;
                case "b":
                    _obj = WarTechPoolsA.poolsA.tech1.GetPooledGameObject();
                    _obj.SetActive(true);
                    _obj.transform.position = transform.position;
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
