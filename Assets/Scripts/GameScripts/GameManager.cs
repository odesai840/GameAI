using UnityEngine;
using UnityEngine.UI;
using TMPro;  // Import TextMesh Pro

public class GameManager : MonoBehaviour
{
    [SerializeField] private TMP_Text timerText;    // UI Text to display the timer
    [SerializeField] private TMP_Text scoreText;    // UI Text to display the score
    [SerializeField] private Button startButton;    // Start button
    [SerializeField] private GameObject player;     // Reference to the player object

    private float timer = 0f;
    private int score = 0;
    private bool isGameRunning = false;
    private int scorePerSecond = 100;               // Score increased per second value
    private float scoreTimer = 0f;                  // Time accumulator for score updates

    void Start()
    {
        // Initialize UI and game state
        timerText.text = "Time: 0.0";
        scoreText.text = "Score: 0";
        startButton.onClick.AddListener(StartGame);
    }

    void Update()
    {
        if (isGameRunning)
        {
            // Update the timer
            timer += Time.deltaTime;
            timerText.text = "Time: " + timer.ToString("F1");

            // Accumulate time to update score
            scoreTimer += Time.deltaTime;

            // Check if 1 second has passed
            if (scoreTimer >= 1f)
            {
                // Update the score by scorePerSecond every second
                AddScore(scorePerSecond);

                // Reset the score timer to track the next second
                scoreTimer -= 1f; // This ensures you update the score once per second
            }
        }
    }

    public void StartGame()
    {
        // Reset the game state
        timer = 0f;
        score = 0;
        isGameRunning = true;

        // Update UI
        scoreText.text = "Score: 0";

        // Respawn player if needed
        player.SetActive(true);
        player.transform.position = Vector3.zero;  // Reset position

        // Hide start button
        startButton.gameObject.SetActive(false);
    }

    public void EndGame()
    {
        isGameRunning = false;

        // Show the start button to allow restarting
        startButton.gameObject.SetActive(true);

        // Optionally deactivate the player (simulate "death")
        player.SetActive(false);
    }

    public void AddScore(int points)
    {
        score += points;
        scoreText.text = "Score: " + score;
    }
}
