using UnityEngine;
using System.Collections.Generic;

public class GroundSpawner : MonoBehaviour
{
    public GameObject groundPrefab;
    public float moveSpeed = 8f;

    private List<GameObject> grounds = new List<GameObject>();
    private float nextZ = 0f;

    void Start()
    {
        // Başlangıçta 3 zemin oluştur
        for (int i = 0; i < 3; i++)
        {
            SpawnGround();
        }
    }

    void Update()
    {
        // Zeminleri hareket ettir
        for (int i = grounds.Count - 1; i >= 0; i--)
        {
            if (grounds[i] != null)
            {
                grounds[i].transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);
            }
        }

        // En arkadaki zemini sil
        if (grounds.Count > 0 && grounds[0] != null && grounds[0].transform.position.z < -20f)
        {
            Destroy(grounds[0]);
            grounds.RemoveAt(0);
            SpawnGround();
        }
    }

    void SpawnGround()
    {
        Vector3 pos = new Vector3(0, 0, nextZ);
        GameObject newGround = Instantiate(groundPrefab, pos, Quaternion.identity);
        grounds.Add(newGround);
        nextZ += 20f;
    }
}