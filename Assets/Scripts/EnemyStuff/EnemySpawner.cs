using System.Collections.Generic;
using Controllers;
using Controllers.Pools;
using MoreMountains.Tools;
using UnityEngine;
using UnityEngine.Serialization;

namespace EnemyStuff
{
    public class EnemySpawner : MonoBehaviour
    {
        public Transform minSpawnPoint, maxSpawnPoint;
        public float spawnInterval;
        public int checkPerFrame;
        public List<WaveInfo> waves;
        
        private readonly List<GameObject> _spawnedEnemies = new();
        private Transform _target;
        private int _enemyToCheck, _currentWave;
        private float _spawnTimer, _waveTimer, _despawnDistance;

    
        private void Start()
        {
            _spawnTimer = spawnInterval;
        
            _target = PlayerHealthController.contPHealth.transform;

            _despawnDistance = Vector3.Distance(transform.position, maxSpawnPoint.position) + 3f;

            _currentWave = -1;
            SpawnNextWave();
        }
    
        private void Update()
        {
            if (PlayerController.contPlayer.gameObject.activeSelf)
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


                        var newEnemy = EnemyPoolManager.PoolEnemys.wavePools[waves[_currentWave].enemyToSpawn].GetPooledGameObject();
                        newEnemy.transform.position = SelectSpawnPoint();
                        newEnemy.SetActive(true);
                    
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
                            _spawnedEnemies[_enemyToCheck].gameObject.SetActive(false);
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

        private Vector3 SelectSpawnPoint()
        {
            var spawnPoint = Vector3.zero;

            var spawnVerticalEdge = Random.Range(0f, 1.0f) > .5f;
            if (spawnVerticalEdge)
            {
                spawnPoint.y = Random.Range(minSpawnPoint.position.y, maxSpawnPoint.position.y);
                spawnPoint.x = Random.Range(0f, 1.0f) > .5f ? maxSpawnPoint.position.x : minSpawnPoint.position.x;
            }
            else
            {
                spawnPoint.x = Random.Range(minSpawnPoint.position.x, maxSpawnPoint.position.x);
                spawnPoint.y = Random.Range(0f, 1.0f) > .5f ? maxSpawnPoint.position.y : minSpawnPoint.position.y;
            }
        
            return spawnPoint;
        }

        private void SpawnNextWave()
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
        public int enemyToSpawn;
        [FormerlySerializedAs("waveSize")] public int waveDuration;
        public float waveInterval;
    }
}