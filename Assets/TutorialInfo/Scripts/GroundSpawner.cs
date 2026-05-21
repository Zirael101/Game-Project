using UnityEngine;
using System.Collections.Generic;

public class GroundSpawner : MonoBehaviour
{
    [Header("Zemin Ayarları")]
    public GameObject groundPrefab;
    public float groundLength = 20f;
    public float moveSpeed = 8f;

    private List<GameObject> grounds = new List<GameObject>();
    private float nextSpawnZ = 0f;

    void Start()
    {
        if (groundPrefab == null)
        {
            Debug.LogError("❌ Ground Prefab atanmamış!");
            return;
        }

        // Başlangıçta 5 zemin oluştur (daha fazla)
        for (int i = 0; i < 5; i++)
        {
            SpawnGround();
        }

        Debug.Log("✅ Başlangıçta " + grounds.Count + " zemin oluşturuldu.");
    }

    void Update()
    {
        // Tüm zeminleri hareket ettir
        for (int i = grounds.Count - 1; i >= 0; i--)
        {
            if (grounds[i] != null)
            {
                grounds[i].transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);
            }
        }

        // Spawn kontrolü: En arkadaki zemin belirli noktaya gelince yeni ekle
        if (grounds.Count > 0)
        {
            GameObject lastGround = grounds[grounds.Count - 1];
            if (lastGround != null && lastGround.transform.position.z < 30) // 30'den küçükse yeni ekle
            {
                SpawnGround();
                Debug.Log("➕ Yeni zemin eklendi! Toplam: " + grounds.Count);
            }
        }

        // Çok arkada kalan zemini sil
        if (grounds.Count > 0)
        {
            GameObject firstGround = grounds[0];
            if (firstGround != null && firstGround.transform.position.z + groundLength < -15)
            {
                grounds.RemoveAt(0);
                Destroy(firstGround);
                Debug.Log("🗑️ Zemin silindi. Kalan zemin: " + grounds.Count);
            }
        }
    }

    void SpawnGround()
    {
        Vector3 pos = new Vector3(0, 0, nextSpawnZ);
        GameObject newGround = Instantiate(groundPrefab, pos, Quaternion.identity);
        grounds.Add(newGround);
        nextSpawnZ += groundLength;
    }
}