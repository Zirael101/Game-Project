using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Takip Ayarları")]
    public Transform target; // Player
    public Vector3 offset = new Vector3(0, 5, -10);
    public float smoothSpeed = 5f;

    void LateUpdate()
    {
        if (target == null) return;

        // Hedef pozisyon = player pozisyonu + offset
        Vector3 desiredPosition = target.position + offset;

        // Yumuşak takip için Lerp kullan
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        // Kamerayı yeni pozisyona taşı
        transform.position = smoothedPosition;
    }
}