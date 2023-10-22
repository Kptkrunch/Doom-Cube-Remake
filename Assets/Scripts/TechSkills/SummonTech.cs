using Controllers;
using Controllers.Pools;

namespace TechSkills
{
    public class SummonTech : Tech
    { 
        private void OnEnable()
        {
            var tech = TechPools.pools.techList[id].GetPooledGameObject();
            tech.SetActive(true);
            tech.transform.position = TechController.contTechCon.transform.position;
            gameObject.SetActive(false);
        }
    }
}
