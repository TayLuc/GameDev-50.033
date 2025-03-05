// GameManager.cs
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("References")]
    public Bullet bulletPrefab;
    public BulletPool bulletPool;

    [Header("Game Events")]
    public GameEvent gameRestartEvent;
    public GameEvent gameOverEvent;

    private void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Initialize bullet pool
        InitializeSystems();
    }

    private void InitializeSystems()
    {
        // Set up bullet pool if it doesn't exist
        if (bulletPool == null)
        {
            GameObject bulletPoolObject = new GameObject("BulletPool");
            bulletPool = bulletPoolObject.AddComponent<BulletPool>();
            bulletPool.bulletPrefab = bulletPrefab;
            bulletPool.totalBullets = 100;
            bulletPool.InitializeBullet();
            DontDestroyOnLoad(bulletPoolObject);
        }
    }

    private bool isRestarting = false;

    public void RestartGame()
    {
        // Prevent recursive calls
        if (isRestarting) return;

        isRestarting = true;

        // Raise the event first
        if (gameRestartEvent != null)
        {
            gameRestartEvent.Raise();
        }

        //Destroy all active bullets
        bulletPool.DestroyAllBullets();

        // Then reload the scene
        SceneManager.LoadScene("CombatScene");

        // Reset the flag (though this won't execute until after scene load)
        isRestarting = false;
    }

    public void GameOver()
    {
        if (gameOverEvent != null)
        {
            gameOverEvent.Raise();
        }
    }
}