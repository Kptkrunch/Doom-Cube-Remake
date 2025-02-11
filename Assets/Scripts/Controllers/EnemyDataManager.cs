using System.Collections;
using System.Collections.Generic;
using ScriptableObjs;
using UnityEngine;

public class EnemyDataManager : MonoBehaviour
{
    public static EnemyDataManager Instance;
    public EnemyDatabase enemyDb;
    private void Awake()
    {
        Instance = this;
    }
}
