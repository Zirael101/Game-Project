using UnityEngine;
using System.Collections.Generic;

public class GroundSpawner : MonoBehaviour
{
    public GameObject groundPrefab;
    public static float moveSpeed = 8f;

    private List<GameObject> grounds = new List<GameObject>();
    private float groundLength = 20f;

    void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            SpawnGround();
        }
    }

    void Update()
    {
        for (int i = grounds.Count - 1; i >= 0; i--)
        {
            if (grounds[i] != null)
            {
                grounds[i].transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);
            }
        }

        if (grounds.Count > 0 && grounds[0] != null)
        {
            if (grounds[0].transform.position.z < -groundLength)
            {
                Destroy(grounds[0]);
                grounds.RemoveAt(0);
                SpawnGround(); 
            }
        }
    }

    void SpawnGround()
    {
        float spawnZ = 0f;
        if (grounds.Count > 0)
        {
            spawnZ = grounds[grounds.Count - 1].transform.position.z + groundLength;
        }

        Vector3 pos = new Vector3(0, 0, spawnZ);
        GameObject newGround = Instantiate(groundPrefab, pos, Quaternion.identity);
        grounds.Add(newGround);
    }

    public static void UpdateSpeed(float newSpeed)
    {
        moveSpeed = newSpeed;
    }
}