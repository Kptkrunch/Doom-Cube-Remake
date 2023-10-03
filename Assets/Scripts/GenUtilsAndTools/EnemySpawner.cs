using System.Collections.Generic;
using Controllers;
using UnityEngine;
using UnityEngine.Serialization;

namespace GenUtilsAndTools
{
    public class EnemySpawner : MonoBehaviour
    {

        public GameObject enemyToSpawn;

        public float spawnInterval;
        private float _spawnTimer;
        public Transform minSpawnPoint, maxSpawnPoint;
    
        private Transform _target;

        private float _despawnDistance;

        private readonly List<GameObject> _spawnedEnemies = new();

        public int checkPerFrame;
        private int _enemyToCheck;

        public List<WaveInfo> waves;
        private int _currentWave;
        private float _waveTimer;
    
        private void Start()
        {
            _spawnTimer = spawnInterval;
        
            _target = PlayerHealthController.phcInstance.transform;

            _despawnDistance = Vector3.Distance(transform.position, maxSpawnPoint.position) + 3f;

            _currentWave = -1;
            SpawnNextWave();
        }
    
        private void Update()
        {
            if (PlayerController.pController.gameObject.activeSelf)
            {
                if (_currentWave < waves.Count)
                {
                    _waveTimer -= Time.deltaTime;
                    if (_waveTimer <= 0)
                    {
                        SpawnNextWave();
                    }

                    _spawnTimer -= Time.deltaTime;
                    if (_spawnTimer <= 0)
                    {
                        _spawnTimer = waves[_currentWave].waveInterval;

                        var newEnemy = Instantiate(waves[_currentWave].enemyToSpawn, SelectSpawnPoint(),
                            Quaternion.identity);
                    
                        _spawnedEnemies.Add(newEnemy);
                    }
                }
            }

            transform.position = _target.position;

            var checkTarget = _enemyToCheck + checkPerFrame;

            while (_enemyToCheck < checkTarget)
            {
                if (_enemyToCheck < _spawnedEnemies.Count)
                {
                    if (!_spawnedEnemies[_enemyToCheck])
                    {
                        if (Vector3.Distance(transform.position, _spawnedEnemies[_enemyToCheck].transform.position) >
                            _despawnDistance)
                        {
                            Destroy(_spawnedEnemies[_enemyToCheck]);
                            _spawnedEnemies.RemoveAt(_enemyToCheck);
                            checkTarget--;
                        }
                        else
                        {
                            _enemyToCheck++;
                        }
                    }
                    else
                    {
                        _spawnedEnemies.RemoveAt(_enemyToCheck);
                        checkTarget--;
                    }
                }
                else
                {
                    _enemyToCheck = 0;
                    checkTarget = 0;
                }
            }
        }

        public Vector3 SelectSpawnPoint()
        {
            var spawnPoint = Vector3.zero;

            var spawnVerticalEdge = Random.Range(0f, 1.0f) > .5f;
            if (spawnVerticalEdge)
            {
                spawnPoint.y = Random.Range(minSpawnPoint.position.y, maxSpawnPoint.position.y);
                if (Random.Range(0f, 1.0f) > .5f)
                {
                    spawnPoint.x = maxSpawnPoint.position.x;
                }
                else
                {
                    spawnPoint.x = minSpawnPoint.position.x;
                }
            }
            else
            {
                spawnPoint.x = Random.Range(minSpawnPoint.position.x, maxSpawnPoint.position.x);
                if (Random.Range(0f, 1.0f) > .5f)
                {
                    spawnPoint.y = maxSpawnPoint.position.y;
                }
                else
                {
                    spawnPoint.y = minSpawnPoint.position.y;
                }
            }
        
            return spawnPoint;
        }

        public void SpawnNextWave()
        {
            _currentWave++;
            if (_currentWave >= waves.Count)
            {
                _currentWave = waves.Count - 1;
            }

            _waveTimer = waves[_currentWave].waveDuration;
            _spawnTimer = waves[_currentWave].waveInterval;
        }
    }

    [System.Serializable]
    public class WaveInfo
    {
        public GameObject enemyToSpawn;
        [FormerlySerializedAs("waveSize")] public int waveDuration;
        public float waveInterval;
    }
}