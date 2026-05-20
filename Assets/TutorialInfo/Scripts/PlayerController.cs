using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Hareket Ayarları")]
    public float runSpeed = 5f;
    public float sideSpeed = 5f;
    public float jumpForce = 5f;

    [Header("Sınırlar")]
    public float leftLimit = -2.5f;
    public float rightLimit = 2.5f;

    private Rigidbody rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
            rb = gameObject.AddComponent<Rigidbody>();

        rb.constraints = RigidbodyConstraints.FreezeRotation;
        rb.velocity = Vector3.forward * runSpeed;
    }
    void Update() {
    
        rb.velocity = new Vector3(0, rb.velocity.y, runSpeed);

        float horizontal = Input.GetAxis("Horizontal");
        Vector3 sideMove = new Vector3(horizontal * sideSpeed * Time.deltaTime, 0, 0);
        transform.Translate(sideMove);

        float clampedX = Mathf.Clamp(transform.position.x, leftLimit, rightLimit);
        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);

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
}