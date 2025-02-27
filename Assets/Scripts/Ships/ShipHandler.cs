using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShipHandler : MonoBehaviour
{
    // Reference to the ScriptableObject
    public EntityStats stats;

    public float currentHealth;
    private bool isAlive;
    public bool isEnemy = true;
    public float firingCooldown; // Track time since last fire
    private bool isFiring; // when the ship is firing, cannot move
    public Vector2 firingPoint;
    public Vector2 firingPointInWorldSpace => transform.position + new Vector3(isEnemy ? -firingPoint.x : firingPoint.x, firingPoint.y);
    Vector3 firingVector => Quaternion.AngleAxis(firingAngle, isEnemy ? Vector3.forward : Vector3.back) * (isEnemy ? Vector3.right : Vector3.left);
    public float firingAngle;
    public float firingVelocity;
    public bool isBase = false;
    public GameObject explosions;
    public AudioClip cannonFireSound;
    public AudioSource audioSource;
    private float explosionDelay = 2f;

    // Reference to the Animator component
    private Animator animator;

    // Reference to targeting component
    private EntityTargetting targeting;

    // For movement
    private Rigidbody2D rb; // Assuming you're using 2D physics

    // Start is called before the first frame update
    void Start()
    {
        // Initialize properties from the stats ScriptableObject
        currentHealth = stats.maxHealth;
        audioSource.clip = cannonFireSound;

        // Get components
        animator = GetComponent<Animator>();
        targeting = GetComponent<EntityTargetting>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if alive

        if (firingCooldown >= 0)
            firingCooldown -= Time.deltaTime;

        // Handle movement
        HandleMovement();
    }

    void HandleMovement()
    {
        if (!rb)
            return; // for the base
        // Check if we have a target and if we're not firing
        if (firingCooldown <= 0 && (targeting == null || targeting.currentTarget == null))
        {
            // No target, move left if player, right if enemy

            Vector2 moveDirection = isEnemy ? Vector2.right : Vector2.left;

            // Use the speed from EntityStats
            rb.velocity = moveDirection * stats.movementSpeed;
        }
        else if (firingCooldown > 0)
        {
            // If firing, stop moving
            rb.velocity = Vector2.zero;
        }
    }

    // Method to take damage
    public void TakeDamage(float amount)
    {
        currentHealth = Mathf.Max(0, currentHealth - amount);
        Debug.Log("taking Damage");

        // Additional effects when taking damage could go here
        // e.g., visual effects, sound effects, etc.

        // Check if destroyed
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Method to fire weapon
    public void Fire()
    {
        if (firingCooldown <= 0)
        {
            // Set firing cooldown based on attack speed
            if (stats.attackSpeed != 0)
                firingCooldown = 1f / stats.attackSpeed;

            // Trigger the firing animation
            if (animator != null)
            {
                audioSource.Play();
                animator.SetTrigger("Firing");
            }
            // Implement firing logic here

            // e.g., instantiate projectile, apply damage, etc.
            BulletPool.Instance.SpawnBullet(stats.attackDamage, stats.bulletSprite, firingPointInWorldSpace, firingVector * firingVelocity, gameObject.tag, gameObject.layer);

        }
    }

    private void Die()
    {
        // Handle destruction logic
        // e.g., play explosion effect, spawn debris, award points, etc.

        // Destroy the game object
        if (isBase)
        {
            explosions.SetActive(true);
            StartCoroutine(DeathDelay());
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawRay(firingPointInWorldSpace, firingVector);
        Gizmos.DrawWireSphere(transform.position, stats.attackRange);
    }

    private IEnumerator DeathDelay()
    {
        yield return new WaitForSeconds(explosionDelay);
        Destroy(gameObject);
    }
}