using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Hareket Ayarları")]
    public float sideSpeed = 6f;
    public float jumpForce = 6f;

    [Header("Sınırlar")]
    public float leftLimit = -2.5f;
    public float rightLimit = 2.5f;

    private Rigidbody rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // Karakterin Z ekseninde hareket etmesini ENGELLE
        rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
    }

    void Update()
    {
        // Sağa/sola hareket
        float horizontal = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * horizontal * sideSpeed * Time.deltaTime);

        // Sınırları kontrol et (çok sağa/sola gitmesin)
        float x = Mathf.Clamp(transform.position.x, leftLimit, rightLimit);
        transform.position = new Vector3(x, transform.position.y, transform.position.z);

        // Zıplama
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    // Önce bir event tanımla (başka script'lerin haberi olsun)
    public delegate void GameOverHandler();
    public static event GameOverHandler OnGameOver;

    // Sonra OnTriggerEnter metodunu ekle
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            GameOver();
        }
    }

    void GameOver()
    {
        Debug.Log("💀 Oyun Bitti!");

        // Oyunu durdur
        Time.timeScale = 0f;

        // Event'i tetikle (başka script'ler haberdar olsun)
        OnGameOver?.Invoke();

        // İstersen burada GameOver paneli açabilirsin
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}