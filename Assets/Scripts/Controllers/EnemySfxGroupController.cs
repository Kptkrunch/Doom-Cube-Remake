using System.Collections.Generic;
using MoreMountains.Feedbacks;
using MoreMountains.Tools;
using UnityEngine;

namespace Controllers
{
    public class EnemySfxGroupController : MonoBehaviour
    {
        public static EnemySfxGroupController Instance;
        public List<MMF_Player> enemySoundPlayerList;
        public List<MMSimpleObjectPooler> enemyAttackParticlePlayerList;
    
        private void Awake()
        {
            Instance = this;
        }

        public void PlayEnemyAttackFeedbacks(int index, Vector2 location)
        {
            var attackObject = enemyAttackParticlePlayerList[index].GetPooledGameObject();
            enemySoundPlayerList[index].PlayFeedbacks(location);
            attackObject.transform.position = location;
            attackObject.SetActive(true);
        }
    }
}
