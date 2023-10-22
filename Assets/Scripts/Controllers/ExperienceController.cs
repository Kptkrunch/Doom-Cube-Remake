using GenUtilsAndTools;
using UI;
using UnityEngine;

namespace Controllers
{
    public class ExperienceController : MonoBehaviour
    {
        public static ExperienceController contExp;
        public LevelController lvlController;
        public ItemDropper expDrop;
        public int currentExp;


        private void Awake()
        {
            contExp = this;
        }

        public void GetExp(int exp)
        {
            currentExp += exp;
        
            ExpBar.expBar.UpdateExpBar(currentExp, lvlController.currentLevel,
                lvlController.expLevels[lvlController.currentLevel]);

            if (currentExp >= lvlController.expLevels[lvlController.currentLevel])
            {
                lvlController.LevelUp();
                if (exp > 0)
                {
                    currentExp = 0;
                }
            }
        }
    }
}
