using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    // Handle the bullets 
    public int totalBullets = 100;
    public bullet bulletPrefab;

    public Stack<bullet> availableBullets = new();

    // Singleton for the bullet pool
    private static BulletPool _instance;
    public static BulletPool Instance
    {
        get
        {
            return _instance;
        }
    }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;

        InitializeBullet();
    }


    public void InitializeBullet()
    {
        for (int i = 0; i < totalBullets; i++)
        {
            bullet bullet = Instantiate(bulletPrefab, transform);
            bullet.gameObject.SetActive(false);
            availableBullets.Push(bullet);
        }
    }


    public void SpawnBullet(float damage, Sprite bulletSprite, Vector2 position, Vector2 velocity, string parentTag, int layer)
    {
        bullet bullet = availableBullets.Pop(); // pop the last availble bullet

        // Instatiate and set the values of the bullet
        bullet.gameObject.SetActive(true);
        bullet.damage = damage;
        bullet.sprite = bulletSprite;
        bullet.transform.position = position;
        bullet.brigidbody2D.velocity = velocity;
        bullet.tag = parentTag == "PlayerEntities" ? "PlayerBullet" : "EnemyBullet";
        bullet.gameObject.layer = layer;
    }

    public void destroyBullet(bullet bullet)
    {
        bullet.gameObject.SetActive(false);
        availableBullets.Push(bullet);// put back into the stack
    }


}
