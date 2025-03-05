using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

public class Bullet : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Rigidbody2D brigidbody2D;
    public AudioSource audioSource;
    public AudioClip bulletBoom;
    public GameObject explosion;
    public float bulletLife = 2f;   // Lifetime of the bullet in seconds
    public float damage = 1f;   // Damage of the bullet

    private string EntityTag => gameObject.tag == "blueBullet" ? "blueEnemy" : "redEnemy";

    public Sprite sprite
    {
        get
        {
            return spriteRenderer.sprite;
        }
        set
        {
            spriteRenderer.sprite = value;
        }
    }

    private void Update()
    {
        // This portion is for making the bullet follow the direction it is heading in
        // float angle = Vector2.Angle(brigidbody2D.velocity, Vector2.left);
        transform.rotation = Quaternion.LookRotation(Vector3.forward, Quaternion.AngleAxis(-90, Vector3.forward) * brigidbody2D.velocity);

        // How long the bullet is alive for
        bulletLife -= Time.deltaTime;
        if (bulletLife <= 0 || transform.position.y < -10)
            BulletPool.Instance.destroyBullet(this);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Debug.Log("Collided with: " + collision.collider.tag);

        // Check if we hit an enemy (regular or special)
        bool hitEnemy = collision.collider.CompareTag(EntityTag) ||
                        collision.collider.CompareTag("specialEnemy");

        if (hitEnemy)
        {
            // Play sound effect if set up
            if (audioSource != null && bulletBoom != null)
            {
                audioSource.clip = bulletBoom;
                audioSource.Play();
            }

            // Show explosion if set up
            if (explosion != null)
            {
                GameObject explosionInstance = Instantiate(explosion, transform.position, Quaternion.identity);
                ParticleSystem particleSystem = explosionInstance.GetComponent<ParticleSystem>();
                if (particleSystem != null)
                {
                    float duration = particleSystem.main.duration;
                    Destroy(explosionInstance, duration);
                }
                else
                {
                    Destroy(explosionInstance, 1.0f); // Default fallback
                }
            }

            // Apply damage
            EnemyController enemy = collision.gameObject.GetComponent<EnemyController>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
                // Debug.Log("Applied damage to enemy: " + collision.collider.tag);
            }
        }

        // Return bullet to pool regardless of what was hit
        BulletPool.Instance.destroyBullet(this);
    }
}