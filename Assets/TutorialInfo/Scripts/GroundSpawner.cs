using UnityEngine;
using System.Collections.Generic;

public class GroundSpawner : MonoBehaviour
{
    [Header("Zemin Ayarları")]
    public GameObject groundPrefab;
    public float groundLength = 20f;
    public int initialGroundCount = 5;

    [Header("Takip")]
    public Transform player;

    private List<GameObject> activeGrounds = new List<GameObject>();
    private float spawnZ = 0f;

    void Start()
    {
        if (groundPrefab == null)
        {
            Debug.LogError("Ground Prefab atanmamış! Lütfen Prefab'ı sürükleyin.");
            return;
        }

        // Başlangıçta 5 tane zemin oluştur
        for (int i = 0; i < initialGroundCount; i++)
        {
            SpawnGround();
        }
    }

    void Update()
    {
        if (player == null || groundPrefab == null) return;

        // Player arkadaki zemine geldiyse yeni zemin ekle
        if (player.position.z + 30 > spawnZ - groundLength)
        {
            SpawnGround();
        }

        // Arkada kalan zemini sil
        for (int i = activeGrounds.Count - 1; i >= 0; i--)
        {
            if (activeGrounds[i] != null &&
                activeGrounds[i].transform.position.z + groundLength < player.position.z - 20)
            {
                Destroy(activeGrounds[i]);
                activeGrounds.RemoveAt(i);
            }
        }
    }

    void SpawnGround()
    {
        Vector3 spawnPosition = new Vector3(0, 0, spawnZ);
        GameObject newGround = Instantiate(groundPrefab, spawnPosition, Quaternion.identity);
        activeGrounds.Add(newGround);
        spawnZ += groundLength;

        Debug.Log("Zemin oluşturuldu: " + spawnPosition); // Test için
    }
}