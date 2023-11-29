using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    EnemySpawner _enemySpawner;

    void Start()
    {
        NewWave();
    }

    void Update()
    {
        
    }

    void NewWave()
    { 
        _enemySpawner.SpawnWave();
    }
}
