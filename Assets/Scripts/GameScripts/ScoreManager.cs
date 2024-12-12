using UnityEngine;
using UnityEngine.UI;
using TMPro;  // Import TextMesh Pro

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText; // UI Text to display the score
    [SerializeField] private int scorePerSecond = 100; // Score increased per second value

    public static int score = 0;
    private float scoreTimer = 0f; // Time accumulator for score updates

    void Start()
    {
        // Initialize UI and game state
        score = 0;
        scoreText.text = "SCORE: 0";
    }

    void Update()
    {
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

    public void AddScore(int points)
    {
        score += points;
        scoreText.text = "SCORE: " + score;
    }
}
