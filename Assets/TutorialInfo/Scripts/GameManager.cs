using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject gameOverPanel;

    private bool isGameOver = false;
    public float currentSpeed = 8f;
    private float speedIncreaseTimer = 0f;
    public float speedIncreaseInterval = 5f;
    public float maxSpeed = 16f;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

        Time.timeScale = 1f;
        currentSpeed = 8f;
        GroundSpawner.UpdateSpeed(currentSpeed);
    }

    void Update()
    {
        if (isGameOver && Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }

        if (!isGameOver)
        {
            speedIncreaseTimer += Time.deltaTime;
            if (speedIncreaseTimer >= speedIncreaseInterval)
            {
                speedIncreaseTimer = 0f;
                currentSpeed = Mathf.Min(maxSpeed, currentSpeed + 0.5f);
                GroundSpawner.UpdateSpeed(currentSpeed);
                Debug.Log(" Hız arttı: " + currentSpeed);
            }
        }
    }

    public void GameOver()
    {
        if (isGameOver) return;
        isGameOver = true;
        Time.timeScale = 0f;

        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        Debug.Log(" Oyun Bitti! R tuşuna bas.");
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}