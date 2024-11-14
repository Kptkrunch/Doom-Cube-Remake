using System.Collections;
using System.Collections.Generic;
using Controllers;
using Controllers.Pools;
using UnityEngine;

namespace EnemyStuff
{
    public class EnemyWaveSpawnerPoint : MonoBehaviour
    {
        public Transform playerTransform;
        public float waveDuration;
        public int currentWaveIndex = 0;
        private bool _canSpawn;
        private float _waveTimer;
        
        
        public List<SpawnPointInfo> waveInfoList = new();

        private void Start()
        {
            _canSpawn = true;
            waveDuration = waveInfoList[currentWaveIndex].waveDuration;
            _waveTimer = waveDuration;
            playerTransform = PlayerHealthController.contPHealth.transform;
        }

        private void FixedUpdate()
        {
            _waveTimer -= Time.fixedDeltaTime;
            
            if (_canSpawn)
            {
                StartCoroutine(SpawnEnemies());
            }
        }

        IEnumerator SpawnEnemies()
        {
            _canSpawn = false;
            yield return new WaitForSeconds(waveInfoList[currentWaveIndex].spawnRate);
            foreach (var index in waveInfoList[currentWaveIndex].enemyIndices)
            {
                var spawnPointIndex = Random.Range(0,waveInfoList[currentWaveIndex].spawnPoints.Count);
                var enemy = EnemyPoolManager.PoolEnemys.wavePools[index].GetPooledGameObject();
                enemy.transform.position = waveInfoList[currentWaveIndex].spawnPoints[spawnPointIndex].position;
                enemy.SetActive(true);
            }
            
            yield return new WaitForSeconds(waveInfoList[currentWaveIndex].spawnCooldown);
            _canSpawn = true;
            if (_waveTimer <= 0)
            {
                currentWaveIndex++;
                _waveTimer = waveInfoList[currentWaveIndex].waveDuration;
            }
        }
    
    
    }

    [System.Serializable]
    public class SpawnPointInfo
    
    {
        public int waveDuration;
        public float spawnRate;
        public float spawnCooldown;
        public List<int> enemyIndices = new();
        public List<Transform> spawnPoints = new();
    }
}