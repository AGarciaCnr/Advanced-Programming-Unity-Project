using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;

    public int maxEnemyLife = 50;
    public int numberOfEnemiesInWave;
    public float spawnRadius;

    public void SpawnWave()
    {
        PoolManager.Load(_enemyPrefab, numberOfEnemiesInWave);
        for (int i = 0; i < numberOfEnemiesInWave; i++)
        {
            Vector2 randomCirclePoint = Random.insideUnitCircle.normalized * spawnRadius;
            Vector3 spawnPosition = new Vector3(randomCirclePoint.x, 0, randomCirclePoint.y) + transform.position;

            Quaternion spawnRotation = Quaternion.Euler(0, Random.Range(0f, 360f), 0);

            GameObject enemyGO = PoolManager.Spawn(_enemyPrefab, spawnPosition, spawnRotation);

            // Accedemos al componente Enemy y usamos el constructor para inicializarlo.
            Enemy enemy = enemyGO.AddComponent<Enemy>();
        }
    }
}
