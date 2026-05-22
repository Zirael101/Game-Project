using UnityEngine;
using System.Collections.Generic;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;
    public float spawnInterval = 2f;

    private float[] lanePositions = { -2.5f, 0f, 2.5f };
    private float timer;
    private List<GameObject> obstacles = new List<GameObject>();

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnObstacles();
            timer = 0;
            spawnInterval = 16f / GroundSpawner.moveSpeed;
        }

        for (int i = obstacles.Count - 1; i >= 0; i--)
        {
            if (obstacles[i] != null)
            {
                obstacles[i].transform.Translate(Vector3.back * GroundSpawner.moveSpeed * Time.deltaTime);

                if (obstacles[i].transform.position.z < -15f)
                {
                    Destroy(obstacles[i]);
                    obstacles.RemoveAt(i);
                }
            }
            else
            {
                obstacles.RemoveAt(i);
            }
        }
    }

    void SpawnObstacles()
    {
        if (obstaclePrefabs.Length == 0) return;

        int obstacleCount = Random.Range(1, 3); 

        List<int> usedLanes = new List<int>();

        for (int i = 0; i < obstacleCount; i++)
        {
            int randomLane;
            do
            {
                randomLane = Random.Range(0, 3);
            }
            while (usedLanes.Contains(randomLane));

            usedLanes.Add(randomLane);
            int randomObstacle = Random.Range(0, obstaclePrefabs.Length);

            float obstacleY = obstaclePrefabs[randomObstacle].transform.position.y;
            Vector3 pos = new Vector3(lanePositions[randomLane], obstacleY, 15f);
            GameObject newObstacle = Instantiate(obstaclePrefabs[randomObstacle], pos, Quaternion.identity);
            obstacles.Add(newObstacle);
        }
    }
}