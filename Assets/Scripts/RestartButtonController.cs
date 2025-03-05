// RestartButton.cs
using UnityEngine;
using UnityEngine.UI;

public class RestartButton : MonoBehaviour
{
    private Button button;

    private void Start()
    {
        button = GetComponent<Button>();

        // Clear any previous listeners and add new one
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(HandleRestartClick);

        Debug.Log("Restart button initialized: " + gameObject.name);
    }

    private void HandleRestartClick()
    {
        Debug.Log("Restart button clicked");

        // Find GameManager (should be persistent)
        if (GameManager.Instance != null)
        {
            Debug.Log("Found GameManager, calling RestartGame()");
            GameManager.Instance.RestartGame();
        }
        else
        {
            Debug.LogError("GameManager.Instance is null! Can't restart game.");
        }
    }
}