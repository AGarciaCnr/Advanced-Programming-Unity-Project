using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        LoadGame();
    }

    // Reinicia los valores y configura el inicio de las oleadas.
    void LoadGame()
    {
        totalPoints = 0;
        waveCount = 0;
        deadEnemiesCount = 0;
        timeBetweenWaves = initialTimeBetweenWaves;
        gameStarted = false;

        // Invoca el inicio de las oleadas con un retraso de 5 segundos.
        Debug.Log("Starting waves...");
        Invoke("StartWaves", 5f);
    }

    // Inicia la repetición de oleadas con un tiempo de espera inicial.
    void StartWaves()
    {
        timeBetweenWaves = initialTimeBetweenWaves;
        InvokeRepeating("NewWave", 0f, timeBetweenWaves);
    }

    // Gestión del Game Over.
    void GameOver()
    {
        Debug.Log("You Lose");

        // Detiene la repetición de oleadas y carga la escena de Game Over.
        CancelInvoke("NewWave");
        SceneManager.LoadGameOver(GetFinalScore());
    }

    // Generar nueva oleada de enemigos.
    void NewWave()
    {
        // Verifica si el jugador ha perdido y muestra la pantalla de Game Over.
        if (deadEnemiesCount < enemiesPerWave && gameStarted)
        {
            GameOver();
            return;
        }

        // Inicia una nueva oleada y actualiza la información en la interfaz de usuario.
        gameStarted = true;
        deadEnemiesCount = 0;
        waveCount++;
        GameCanvasManager.instance.UpdateWave(waveCount);
        Debug.Log($"Starting wave {waveCount}...");
        Debug.Log($"Wave: {waveCount}, Total points: {totalPoints}");
        _enemySpawner.SpawnWave(enemiesPerWave);

        // Ajusta el tiempo entre oleadas para la próxima vez.
        timeBetweenWaves = Mathf.Max(timeBetweenWaves * 0.9f, minimumTimeBetweenWaves);
        CancelInvoke("NewWave");
        InvokeRepeating("NewWave", timeBetweenWaves, timeBetweenWaves);
    }

    // Actualiza los puntos y la interfaz de usuario cuando un enemigo muere.
    public void OnEnemyDeath(Enemy deadEnemy)
    {
        totalPoints += deadEnemy.Points;
        GameCanvasManager.instance.UpdateScore(totalPoints);
        deadEnemiesCount++;
    }

    // Retorna la puntuación final del jugador.
    public int GetFinalScore()
    {
        return totalPoints;
    }
}
