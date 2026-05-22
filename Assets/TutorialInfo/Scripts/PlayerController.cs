using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float laneChangeSpeed = 15f;
    public float jumpForce = 7f;

    private float[] lanePositions = { -2.5f, 0f, 2.5f };
    private int currentLane = 1;
    private Rigidbody rb;
    private bool isGrounded;
    private bool jumpRequested;
    private float groundCheckDistance = 0.2f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
        transform.position = new Vector3(0, 0.3f, 0);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            currentLane = Mathf.Max(0, currentLane - 1);
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            currentLane = Mathf.Min(2, currentLane + 1);

        Vector3 targetPos = new Vector3(lanePositions[currentLane], transform.position.y, 0);
        transform.position = Vector3.MoveTowards(transform.position, targetPos, laneChangeSpeed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpRequested = true;
            Debug.Log("Zıplama isteği alındı!"); 
        }

        CheckIfGrounded();
    }

    void FixedUpdate()
    {
        if (jumpRequested)
        {
            if (isGrounded)
            {
                rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                isGrounded = false;
                Debug.Log("Zıpladım! Güç: " + jumpForce);
            }
            jumpRequested = false;
        }
    }

    void CheckIfGrounded()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, groundCheckDistance + 0.3f))
        {
            if (hit.collider.CompareTag("Ground"))
            {
                if (!isGrounded)
                {
                    isGrounded = true;
                    Debug.Log("Yere değdi!");
                }
                return;
            }
        }

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            Debug.Log("OnCollisionEnter: Yerdeyim!");
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
            Debug.Log("OnCollisionExit: Havadayım!");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            float obstacleY = other.transform.position.y;
            bool isSameLane = Mathf.Abs(transform.position.x - other.transform.position.x) < 0.8f;

            if (obstacleY > 1f) 
            {
                if (!isGrounded && isSameLane) 
                {
                    Debug.Log(" Havada engele kafa attın!");
                    GameManager.Instance.GameOver();
                }
                else if (!isGrounded && !isSameLane)
                {
                    Debug.Log(" Yan şeritte havada engelin yanından geçtin!");
                }
                else if (isGrounded && isSameLane) 
                {
                    Debug.Log(" Havadaki engelin altından geçtin!");
                    Destroy(other.gameObject);
                }
                else 
                {
                    Debug.Log(" Yan şeritteki havada engele çarptın!");
                    GameManager.Instance.GameOver();
                }
            }
            else 
            {
                if (isSameLane)
                {
                    Debug.Log(" Yerdeki engele çarptın!");
                    GameManager.Instance.GameOver();
                }
                else
                {
                    Debug.Log(" Yan şeritteki engelin yanından geçtin!");
                }
            }
        }
    }
}