using UnityEngine;

namespace TechSkills
{
    public class Tech : MonoBehaviour
    {
        public int techLevel;
        public string description;
        public Sprite techImage;
        public int costMeat, costMetal, costMineral, costPlastic, costEnergy;
        private GameObject _obj;
        
        public virtual void ActivateTech(string button)
        {
            switch (button)
            {
                case "a":
                    _obj = WarTechPoolsA.poolsA.tech1.GetPooledGameObject();
                    _obj.gameObject.SetActive(true);
                    _obj.transform.position = transform.position;
                    break;
                case "x":
                    _obj = WarTechPoolsA.poolsA.tech1.GetPooledGameObject();
                    _obj.gameObject.SetActive(true);
                    _obj.transform.position = transform.position;
                    break;
                case "y":
                    _obj = WarTechPoolsA.poolsA.tech1.GetPooledGameObject();
                    _obj.gameObject.SetActive(true);
                    _obj.transform.position = transform.position;
                    break;
                case "b":
                    _obj = WarTechPoolsA.poolsA.tech1.GetPooledGameObject();
                    _obj.gameObject.SetActive(true);
                    _obj.transform.position = transform.position;
                    break;
            }
        }
        
        public void UpdateTech()
        {
        
        }
    }
}
