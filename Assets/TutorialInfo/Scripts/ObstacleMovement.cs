using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    [HideInInspector]
    public float moveSpeed = 8f;

    void Update()
    {
        // Engeli karaktere doğru hareket ettir
        transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);
    }
}