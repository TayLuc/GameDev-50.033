// EnemySpawner.cs
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject redEnemyPrefab;
    public GameObject blueEnemyPrefab;
    public GameObject specialEnemyPrefab;

    public float spawnRate = 0.5f;
    public float specialEnemyChance = 0.1f; // 5% chance to spawn a special enemy

    public float minX = -10f;
    public float maxX = 10f;
    public float minY = -6f;
    public float maxY = 6f;
    private const float maxSpawnRate = 5f;
    private const float rateIncreaseInterval = 5f;
    private const float rateIncreaseAmount = 0.1f;
    private float timeSinceLastSpawn = 0f;
    private float timeSinceLastRateIncrease = 0f;

    private void Update()
    {
        timeSinceLastSpawn += Time.deltaTime;
        timeSinceLastRateIncrease += Time.deltaTime;

        if (timeSinceLastSpawn >= 1f / spawnRate)
        {
            SpawnEnemy();
            timeSinceLastSpawn = 0f;
        }

        if (timeSinceLastRateIncrease >= rateIncreaseInterval)
        {
            spawnRate = Mathf.Min(maxSpawnRate, spawnRate + rateIncreaseAmount);
            timeSinceLastRateIncrease = 0f;
        }
    }

    private void SpawnEnemy()
    {
        Vector2 spawnPosition = GetRandomSpawnPosition();
        GameObject enemyPrefab;

        if (Random.value <= specialEnemyChance)
        {
            enemyPrefab = specialEnemyPrefab;
        }
        else
        {
            enemyPrefab = Random.value <= 0.5f ? redEnemyPrefab : blueEnemyPrefab;
        }

        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }

    private Vector2 GetRandomSpawnPosition()
    {
        // Randomly choose a side (0 = top, 1 = right, 2 = left)
        int side = Random.Range(0, 3);
        Vector2 position = Vector2.zero;

        switch (side)
        {
            case 0: // Top
                position = new Vector2(Random.Range(minX, maxX), maxY);
                break;
            case 1: // Right
                position = new Vector2(maxX, Random.Range(minY, maxY));
                break;
            case 2: // Left
                position = new Vector2(minX, Random.Range(minY, maxY));
                break;
        }

        return position;
    }
}