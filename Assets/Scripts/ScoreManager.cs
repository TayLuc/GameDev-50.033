using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [Header("Score Data")]
    public IntVariable playerScore;
    public GameEvent scoreChangedEvent;
    public AudioSource scoreSound;
    public AudioClip scoreClip;

    [Header("Settings")]
    public int initialScore = 0;

    private void Start()
    {
        // Initialize score when the game starts
        ResetScore();
    }

    public void AddPoints(int points)
    {
        if (playerScore != null)
        {
            playerScore.Value += points;
            // Play score sound
            if (scoreSound != null && scoreClip != null)
            {
                scoreSound.PlayOneShot(scoreClip);
            }

            // Notify listeners that score has changed
            if (scoreChangedEvent != null)
            {
                scoreChangedEvent.Raise();
            }
        }
    }

    public void ResetScore()
    {
        if (playerScore != null)
        {
            playerScore.Value = initialScore;

            // Notify listeners that score has changed
            if (scoreChangedEvent != null)
            {
                scoreChangedEvent.Raise();
            }
        }
    }
}