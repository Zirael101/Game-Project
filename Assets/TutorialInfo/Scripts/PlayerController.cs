using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float laneChangeSpeed = 10f;
    public float jumpForce = 5f;

    private float[] lanePositions = { -2.5f, 0f, 2.5f };
    private int currentLane = 1;
    private Rigidbody rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Tüm hareketleri sıfırla
        rb.velocity = Vector3.zero;

        // Sadece Z eksenini dondur
        rb.constraints = RigidbodyConstraints.FreezePositionZ;

        // Pozisyonu ayarla
        transform.position = new Vector3(0, 0.3f, 0);

        // Yerçekimini normal yap
        rb.useGravity = true;
    }

    void Update()
    {
        // Şerit değiştirme
        if (Input.GetKeyDown(KeyCode.A)) currentLane = Mathf.Max(0, currentLane - 1);
        if (Input.GetKeyDown(KeyCode.D)) currentLane = Mathf.Min(2, currentLane + 1);

        // Yumuşak geçiş
        Vector3 targetPos = new Vector3(lanePositions[currentLane], transform.position.y, 0);
        transform.position = Vector3.Lerp(transform.position, targetPos, laneChangeSpeed * Time.deltaTime);

        // Zıplama
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            GameManager.Instance.GameOver();
        }
    }
}