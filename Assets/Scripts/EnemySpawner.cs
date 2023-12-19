using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Radio de spawn y lista de tipos de enemigos disponibles.
    const float SPAWN_RADIUS = 4f;
    private int _numberOfEnemies;
    [SerializeField] private List<Enemy> _enemies = new List<Enemy>();

    // Genera una oleada de enemigos con tipos aleatorios.
    public void SpawnWave(int numberOfEnemies)
    {
        _numberOfEnemies = numberOfEnemies;
        for (int i = 0; i < numberOfEnemies; i++)
        {
            // Selecciona aleatoriamente un tipo de enemigo y lo spawnea.
            int randomEnemy = Random.Range(0, 3);
            switch (randomEnemy)
            {
                case 0:
                    SpawnEnemy(_enemies[0]);
                    break;
                case 1:
                    SpawnEnemy(_enemies[1]);
                    break;
                case 2:
                    SpawnEnemy(_enemies[2]);
                    break;
            }
        }
    }

    // Spawnea un enemigo en una posición aleatoria dentro del radio.
    void SpawnEnemy(Enemy enemy)
    {
        LoadEnemyPrefab(enemy.gameObject);

        Vector2 randomCirclePoint = Random.insideUnitCircle.normalized * SPAWN_RADIUS;
        Vector3 spawnPosition = new Vector3(randomCirclePoint.x, 0, randomCirclePoint.y) + transform.position;
        Quaternion spawnRotation = Quaternion.Euler(0, Random.Range(0f, 360f), 0);

        enemy.Spawn(spawnPosition, spawnRotation);
    }

    // Carga el prefab del enemigo en el PoolManager si aún no está cargado.
    void LoadEnemyPrefab(GameObject enemyPrefab)
    {
        if (enemyPrefab == null)
        {
            Debug.LogError("Enemy prefab is null.");
            return;
        }
        if (PoolManager.GetInstanceCount(enemyPrefab) > _numberOfEnemies)
        {
            Debug.LogWarning("Enemy prefab is already loaded.");
            return;
        }
        PoolManager.Load(enemyPrefab.gameObject, _numberOfEnemies);
    }
}
