using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjs
{
    [CreateAssetMenu(fileName = "EnemyDatabase", menuName = "ScriptableObjects/Enemy Database", order = 1)]
    public class EnemyDatabase : ScriptableObject
    {
        [System.Serializable]
        public class EnemyData
        {
            public Sprite sprite;
            public string enemyName;
            public int collisionAttackIndex;
            public int rangedAttackIndex;
            public int specialAttackIndex;
            public int ammo;
            public int rateOfFire;
            public float range;
            public float damage;
            public float moveSpeed;
            public float health;

            [System.NonSerialized] // Prevent from being serialized by Unity since Dictionary is not supported in the editor
            public Dictionary<string, bool> DamageTypeStatus;
        }

        [SerializeField] private List<EnemyData> enemiesList = new();

        private Dictionary<string, EnemyData> _enemiesDictionary;

        // Ensures the dictionary is populated when the scriptable object is loaded
        private void OnEnable()
        {
            InitializeDictionary();
        }

        private void InitializeDictionary()
        {
            if (_enemiesDictionary != null) return;
            _enemiesDictionary = new Dictionary<string, EnemyData>();
            foreach (var enemy in enemiesList)
            {
                _enemiesDictionary[enemy.enemyName] = enemy;
            }
        }

        // Helper method to retrieve enemy data in O(1)
        public EnemyData GetEnemyData(string enemyName)
        {
            InitializeDictionary();
            _enemiesDictionary.TryGetValue(enemyName, out var enemyData);
            return enemyData;
        }
    }
}