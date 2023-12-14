using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField]
    private Text scoreText;

    void Start()
    {
        // Retrieve the score from PlayerPrefs and display it
        int playerScore = PlayerPrefs.GetInt("PlayerScore", 0);
        scoreText.text = "Final Score: " + playerScore.ToString();
    }
}

