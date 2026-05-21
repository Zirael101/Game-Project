using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0, 5, -10);

    void LateUpdate()
    {
        if (target == null) return;

        // Kamera sabit, sadece hedefin X'ini takip et (isteğe bağlı)
        Vector3 targetPos = new Vector3(target.position.x + offset.x, offset.y, offset.z);
        transform.position = targetPos;
    }
}