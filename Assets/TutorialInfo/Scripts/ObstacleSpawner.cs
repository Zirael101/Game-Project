using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [Header("Engel Ayarları")]
    public GameObject[] obstaclePrefabs;
    public float spawnInterval = 1.5f;
    public float spawnRangeX = 2f;
    public float spawnZ = 15f;
    public float obstacleSpeed = 8f; // Direkt buradan hızı ayarla

    private float timer;
    private bool isSpawning = true;

    void Update()
    {
        if (!isSpawning) return;

        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnObstacle();
            timer = 0;

            // Zorluk arttıkça daha sık engel çıkar
            spawnInterval = Mathf.Max(0.5f, spawnInterval - 0.005f);
        }
    }

    void SpawnObstacle()
    {
        if (obstaclePrefabs.Length == 0) return;

        int randomIndex = Random.Range(0, obstaclePrefabs.Length);
        float randomX = Random.Range(-spawnRangeX, spawnRangeX);
        Vector3 spawnPos = new Vector3(randomX, 0.5f, spawnZ);

        GameObject newObstacle = Instantiate(obstaclePrefabs[randomIndex], spawnPos, Quaternion.identity);

        // Engel hareket script'ini ekle ve hızını ata
        ObstacleMovement movement = newObstacle.AddComponent<ObstacleMovement>();
        movement.moveSpeed = obstacleSpeed; // Artık burada tanımlı

        // 10 saniye sonra yok ol
        Destroy(newObstacle, 10f);
    }

    public void StopSpawning()
    {
        isSpawning = false;
    }

    public void StartSpawning()
    {
        isSpawning = true;
    }
}