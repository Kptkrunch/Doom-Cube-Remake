using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public GameObject enemyToSpawn;

    public float spawnInterval;
    private float _spawnTimer;
    public Transform minSpawnPoint, maxSpawnPoint;
    
    private Transform _target;

    private float _despawnDistance;

    private List<GameObject> _spawnedEnemies = new List<GameObject>();

    public int checkPerFrame;
    private int _enemyToCheck;
    
    void Start()
    {
        _spawnTimer = spawnInterval;
        
        _target = PlayerHealthController.phcInstance.transform;

        _despawnDistance = Vector3.Distance(transform.position, maxSpawnPoint.position) + 3f;
    }
    
    void Update()
    {
        _spawnTimer -= Time.deltaTime;
        if (_spawnTimer <= 0)
        {
            _spawnTimer = spawnInterval;
            
            GameObject newEnemy = Instantiate(enemyToSpawn, SelectSpawnPoint(), transform.rotation);
            _spawnedEnemies.Add(newEnemy);
        }

        transform.position = _target.position;

        int checkTarget = _enemyToCheck + checkPerFrame;

        while (_enemyToCheck < checkTarget)
        {
            if (_enemyToCheck < _spawnedEnemies.Count)
            {
                if (_spawnedEnemies[_enemyToCheck] != null)
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
        Vector3 spawnPoint = Vector3.zero;

        bool spawnVerticalEdge = Random.Range(0f, 1.0f) > .5f;
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
}
