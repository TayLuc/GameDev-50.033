using UnityEngine;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    public IntVariable playerScore;
    public TextMeshProUGUI scoreText;
    public string scorePrefix = "Score: ";

    private void Start()
    {
        UpdateScoreDisplay();
    }

    // This method will be called by the OnScoreChanged event
    public void UpdateScoreDisplay()
    {
        if (scoreText != null && playerScore != null)
        {
            scoreText.text = scorePrefix + playerScore.Value.ToString();
        }
    }
}