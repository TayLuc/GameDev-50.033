// PowerupController.cs
using UnityEngine;

public class PowerupController : MonoBehaviour
{
    public PowerupType powerupType = PowerupType.FireRateBoost;
    public float lifetime = 5f; // How long the powerup stays in the world

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Apply powerup to the player
            PlayerStateController player = collision.GetComponent<PlayerStateController>();
            if (player != null)
            {
                ApplyPowerup(player);
            }

            Destroy(gameObject);
        }
    }

    private void ApplyPowerup(PlayerStateController player)
    {
        player.SetPowerup(powerupType);
    }
}