using ScriptableObjs;
using UnityEngine;

namespace Controllers
{
    public class EnemyDataManager : MonoBehaviour
    {
        public static EnemyDataManager Instance;
        public EnemyDatabase enemyDb;
        private void Awake()
        {
            Instance = this;
        }
    }
}
