using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

public class bullet : MonoBehaviour
{
    public float damage;
    public SpriteRenderer spriteRenderer;
    public Rigidbody2D brigidbody2D;
    public AudioSource audioSource;
    public AudioClip bulletBoom;
    public GameObject explosion;

    private string enemyTag => gameObject.tag == "PlayerBullet" ? "EnemyEntities" : "PlayerEntities";

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
        float angle = Vector2.Angle(brigidbody2D.velocity, Vector2.left);
        transform.rotation = Quaternion.LookRotation(Vector3.forward, Quaternion.AngleAxis(-90, Vector3.forward) * brigidbody2D.velocity);
        // transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        if (transform.position.y < -10)
            BulletPool.Instance.destroyBullet(this);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == enemyTag)
        {
            //play sound
            audioSource.clip = bulletBoom;
            audioSource.Play();

            GameObject explosionInstance = Instantiate(explosion, transform.position, Quaternion.identity);
            ParticleSystem particleSystem = explosionInstance.GetComponent<ParticleSystem>();
            float duration = particleSystem.main.duration;
            Destroy(explosionInstance, duration);


            // Call the damage
            ShipHandler enemyShip = collision.collider.GetComponent<ShipHandler>();
            enemyShip.TakeDamage(damage);

            BulletPool.Instance.destroyBullet(this);
        }
    }

}
