// EnemyController.cs
using UnityEditor.UI;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int health = 1;
    public float moveSpeed = 1f;
    public bool isSpecial = false;
    public Sprite specialSprite;
    public PowerupType powerupType = PowerupType.FireRateBoost;

    [Header("Tags")]
    public string enemyTag = "redEnemy"; // or "redEnemy"

    private Transform player;
    private SpriteRenderer spriteRenderer;
    private PlayerStateController playerController;
    public State poweredUpState;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerController = player.GetComponent<PlayerStateController>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Set the appropriate tag
        gameObject.tag = enemyTag;

        // If this is a special enemy, use a different color
        if (isSpecial)
        {
            spriteRenderer.sprite = specialSprite;
        }
    }

    private void Update()
    {
        if (player != null)
        {
            // Simple AI: Move towards the player
            Vector2 direction = (player.position - transform.position).normalized;
            transform.Translate(moveSpeed * Time.deltaTime * direction);
        }
    }

    public void TakeDamage(float damage)
    {
        health -= Mathf.RoundToInt(damage);

        if (health <= 0)
        {
            Die();
        }
    }

    private void ApplyPowerupDirectly()
    {
        // Apply the powerup directly to the player
        playerController.SetPowerup(powerupType);
    }


    private void Die()
    {
        // If the death is by nuzzling the player
        if (health > 0)
        {
            Destroy(gameObject);
            return;
        }

        // player KILL THE ENEMY
        ScoreManager scoreManager = FindObjectOfType<ScoreManager>();
        if (scoreManager != null)
        {
            scoreManager.AddPoints(1);
        }

        if (isSpecial && playerController != null)
        {
            ApplyPowerupDirectly();
        }

        Destroy(gameObject, 0.01f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Debug.Log("Collided with: " + collision.collider.tag);
        // Check if hit by a bullet matching the same team
        if (collision.collider.CompareTag("Player"))
        {
            playerController.TakeDamage(1);
            Die();
        }
    }
}
