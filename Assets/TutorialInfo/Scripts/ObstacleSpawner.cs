using UnityEngine;
using System.Collections.Generic;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject[] obstaclePrefabs; // Farklı engel tipleri
    public float spawnInterval = 2f;
    public float moveSpeed = 8f;

    private float[] lanePositions = { -2.5f, 0f, 2.5f };
    private float timer;
    private List<GameObject> obstacles = new List<GameObject>();

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnObstacles(); // Çoklu engel oluştur
            timer = 0;

            // Zorluk arttıkça daha sık spawn
            spawnInterval = Mathf.Max(0.7f, spawnInterval - 0.01f);
        }

        // Engelleri hareket ettir
        for (int i = obstacles.Count - 1; i >= 0; i--)
        {
            if (obstacles[i] != null)
            {
                obstacles[i].transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);

                if (obstacles[i].transform.position.z < -10)
                {
                    Destroy(obstacles[i]);
                    obstacles.RemoveAt(i);
                }
            }
        }
    }

    void SpawnObstacles()
    {
        if (obstaclePrefabs.Length == 0) return;

        // KAÇ ENGEL ÇIKACAĞINI RASTGELE SEÇ (1, 2 veya 3)
        int obstacleCount = Random.Range(1, 4);

        // Hangi şeritlerin dolu olduğunu takip et
        List<int> usedLanes = new List<int>();

        for (int i = 0; i < obstacleCount; i++)
        {
            // Kullanılmayan şerit seç
            int randomLane;
            do
            {
                randomLane = Random.Range(0, 3);
            }
            while (usedLanes.Contains(randomLane));

            usedLanes.Add(randomLane);

            // Rastgele engel tipi seç
            int randomObstacle = Random.Range(0, obstaclePrefabs.Length);

            // Engel pozisyonu
            Vector3 pos = new Vector3(lanePositions[randomLane], 0.5f, 15f);

            // Engel oluştur
            GameObject newObstacle = Instantiate(obstaclePrefabs[randomObstacle], pos, Quaternion.identity);
            obstacles.Add(newObstacle);

            // 6 saniye sonra yok et
            Destroy(newObstacle, 6f);
        }

        Debug.Log("🚧 " + obstacleCount + " engel oluştu! Şeritler: " + string.Join(",", usedLanes));
    }
}