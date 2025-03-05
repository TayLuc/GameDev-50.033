using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    // Handle the bullets 
    public int totalBullets = 100;
    public Bullet bulletPrefab;

    public Stack<Bullet> availableBullets = new();

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
            Bullet bullet = Instantiate(bulletPrefab, transform);
            bullet.gameObject.SetActive(false);
            availableBullets.Push(bullet);
        }
    }


    public void SpawnBullet(float damage, Sprite bulletSprite, Vector2 position, Vector2 velocity, string parentTag, int layer)
    {
        Bullet bullet = availableBullets.Pop(); // pop the last availble bullet

        // Instatiate and set the values of the bullet
        bullet.gameObject.SetActive(true);
        bullet.sprite = bulletSprite;
        bullet.transform.position = position;
        bullet.brigidbody2D.velocity = velocity;
        bullet.tag = parentTag == "blueGun" ? "blueBullet" : "redBullet";
        bullet.gameObject.layer = parentTag == "blueGun" ? 9 : 8;// 9 is bluebullet, 8 is redbullet
        bullet.bulletLife = 2f;
    }

    public void destroyBullet(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
        availableBullets.Push(bullet);// put back into the stack
    }

    public void DestroyAllBullets()
    {
        // Find all active bullets
        Bullet[] activeBullets = FindObjectsOfType<Bullet>();

        // Return each to the pool
        foreach (Bullet bullet in activeBullets)
        {
            if (bullet.gameObject.activeSelf)
            {
                destroyBullet(bullet);
            }
        }
    }


}
