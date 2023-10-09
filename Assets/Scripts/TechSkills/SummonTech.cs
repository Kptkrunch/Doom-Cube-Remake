namespace TechSkills
{
    public class SummonTech : Tech
    { 
        private void OnEnable()
        {
            var tech = WarTechPoolsA.poolsA.tech1.GetPooledGameObject();
            tech.SetActive(true);
            tech.transform.position = TechController.contTechCon.transform.position;
            gameObject.SetActive(false);
        }
    }
}
