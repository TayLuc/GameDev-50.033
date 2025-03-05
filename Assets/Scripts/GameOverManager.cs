using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public string gameOverSceneName = "GameOver";
    public float delayBeforeTransition = 2f; // Optional delay before scene change

    // This will be triggered by the OnPlayerDeath event
    public void HandlePlayerDeath()
    {
        // Load game over scene after delay
        Invoke("LoadGameOverScene", delayBeforeTransition);
    }

    private void LoadGameOverScene()
    {
        SceneManager.LoadScene(gameOverSceneName);
    }
}