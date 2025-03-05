// PlayerStateController.cs
using TMPro;
using UnityEditor;
using UnityEngine;

public enum PowerupType
{
    FireRateBoost = 0,
    Default = -1
}

public enum PlayerState
{
    Normal = 0,
    PoweredUp = 1
}

public class PlayerStateController : StateController
{
    public PowerupType currentPowerupType = PowerupType.Default;

    [Header("Player Settings")]
    public float normalFireRate = 1f;
    public float poweredUpFireRate = 3f;
    public float currentFireRate;
    public float powerupDuration = 5f;
    public float bulletSpeed = 10f;
    public float bulletDamage = 1f;

    [Header("Health Settings")]
    public int maxHealth = 100;
    private int currentHealth;

    [Header("Red Bullet Settings")]
    public Sprite RedBulletSprite;
    public string RedGunTag = "redGun"; // or "redGun" - determines bullet team
    public Transform RedFirePoint;
    public int RedBulletLayer = 8; // Default bullet layer

    [Header("Blue Bullet Settings")]
    public Sprite BlueBulletSprite;
    public string BlueGunTag = "blueGun"; // or "redGun" - determines bullet team
    public int bulletLayer = 9; // Default bullet layer
    public Transform BlueFirePoint;

    private float timeSinceLastShot = 0f;
    private Camera mainCamera;
    private Vector2 aimDirection;
    private GameObject energySprite;
    public TextMeshProUGUI healthText;
    public GameEvent playerDeathEvent;
    public AudioSource audioSource;
    public AudioClip gunShotSound;
    public AudioClip damageSound;
    public AudioClip FinalDeathSound;
    public GameObject deathEffect;

    public override void Start()
    {
        base.Start();
        mainCamera = Camera.main;
        energySprite = GameObject.FindGameObjectWithTag("energy");
        if (energySprite != null)
        {
            energySprite.SetActive(false);
        }
        Debug.Log("Energy Sprite: " + energySprite);
        currentFireRate = normalFireRate;
        currentHealth = maxHealth;

        // Ensure BulletPool is in the scene
        if (BulletPool.Instance == null)
        {
            Debug.LogError("BulletPool is missing from the scene!");
        }
    }

    private void Update()
    {
        if (!isActive) return;

        // Aim toward mouse position
        AimTowardsMouse();

        // Auto fire based on fire rate
        timeSinceLastShot += Time.deltaTime;
        if (timeSinceLastShot >= 1f / currentFireRate)
        {
            FireBullet();
            timeSinceLastShot = 0f;
        }

        // Update state machine
        currentState.UpdateState(this);
    }

    private void AimTowardsMouse()
    {
        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;

        aimDirection = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    public void FireBullet()
    {
        if (audioSource != null && gunShotSound != null)
        {
            audioSource.PlayOneShot(gunShotSound);
        }
        if (RedFirePoint != null && BulletPool.Instance != null)
        {
            // Calculate velocity based on aim direction
            Vector2 bulletVelocity = aimDirection * bulletSpeed;

            // Request a bullet from the pool
            BulletPool.Instance.SpawnBullet(
                bulletDamage,
                RedBulletSprite,
                RedFirePoint.position,
                bulletVelocity,
                RedGunTag,
                bulletLayer
            );

            BulletPool.Instance.SpawnBullet(
                bulletDamage,
                BlueBulletSprite,
                BlueFirePoint.position,
                -bulletVelocity,
                BlueGunTag,
                bulletLayer
            );
        }
    }

    public void SetPowerup(PowerupType powerupType)
    {
        currentPowerupType = powerupType;
    }

    public void SetNormalFireRate()
    {
        currentFireRate = normalFireRate;
        if (energySprite != null)
            energySprite.SetActive(false);
    }

    public void SetPoweredUpFireRate()
    {
        currentFireRate = poweredUpFireRate;

        if (energySprite != null)
            energySprite.SetActive(true);
    }

    public void TakeDamage(int damage)
    {
        if (audioSource != null && damageSound != null)
        {
            audioSource.PlayOneShot(damageSound);
        }
        currentHealth -= damage;
        currentHealth = Mathf.Max(0, currentHealth);
        healthText.text = "Health: " + currentHealth;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Handle player death (e.g., play animation, disable player, etc.)
        Debug.Log("Player has died.");
        // Trigger death event
        if (playerDeathEvent != null)
        {
            audioSource.PlayOneShot(FinalDeathSound);
            playerDeathEvent.Raise();

            GameObject deathInstance = Instantiate(deathEffect, transform.position, Quaternion.identity);
            ParticleSystem particleSystem = deathInstance.GetComponent<ParticleSystem>();
            float duration = particleSystem.main.duration;
            Destroy(deathInstance, duration);
        }
    }

    public void ResetPlayer()
    {
        // Reset health
        currentHealth = maxHealth;
        // Reset state
        currentPowerupType = PowerupType.Default;
        TransitionToState(startState);

        // fireRate
        currentFireRate = normalFireRate;
    }
}