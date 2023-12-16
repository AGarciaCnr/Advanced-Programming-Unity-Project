using System.Collections;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    private EnemySpawner _enemySpawner;

    public int totalPoints = 0;
    private int waveCount = 0; // Contador de oleadas

    // Configuración para el tiempo entre oleadas
    public float initialTimeBetweenWaves = 20f;
    public float minimumTimeBetweenWaves = 5f;
    private float timeBetweenWaves;

    public int enemiesPerWave = 5;
    private int deadEnemiesCount = 0;
    private bool gameStarted = false;

    void Start()
    {
        // Introduce un retraso de 5 segundos antes de comenzar la primera oleada
        Debug.Log("Starting waves...");
        Invoke("StartWaves", 5f);
    }

    void Update()
    {
        Debug.Log($"Wave: {waveCount}, Total points: {totalPoints}");
    }

    void StartWaves()
    {
        // Configura el primer tiempo entre oleadas y comienza la repetición
        timeBetweenWaves = initialTimeBetweenWaves;
        InvokeRepeating("NewWave", 0f, timeBetweenWaves);
    }

    void NewWave()
    {
        if (deadEnemiesCount < enemiesPerWave && gameStarted)
        {
            Debug.Log("You Loose");
            Application.Quit();
        }
        gameStarted = true;
        deadEnemiesCount = 0;
        waveCount++; // Incrementa el contador de oleadas
        Debug.Log($"Starting wave {waveCount}...");
        _enemySpawner.SpawnWave(enemiesPerWave);

        // Ajusta el tiempo entre oleadas para la próxima vez
        timeBetweenWaves = Mathf.Max(timeBetweenWaves * 0.9f, minimumTimeBetweenWaves);
        CancelInvoke("NewWave");
        InvokeRepeating("NewWave", timeBetweenWaves, timeBetweenWaves);
    }

    public void OnEnemyDeath(Enemy deadEnemy)
    {
        totalPoints += deadEnemy.Points;
        deadEnemiesCount++;
    }
}
