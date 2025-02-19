using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class addBullet : MonoBehaviour
{

    public Sprite newSprite; // New sprite to be assigned
    public SpriteRenderer spriteRenderer; // Reference to SpriteRenderer
    private CircleCollider2D myCollider; // reference to own circle collider

    private void OnEnable()
    {
        spriteRenderer = GetComponentInParent<SpriteRenderer>();
        EventManager.OnBulletDropped += CheckBulletCollision;
        myCollider = GetComponent<CircleCollider2D>(); // Cache reference to own collider
    }

    private void OnDisable()
    {
        EventManager.OnBulletDropped -= CheckBulletCollision;
    }



    private void CheckBulletCollision(Collider2D bulletCollider)
    {
        // Debug.Log("Addbullet checking for overlap");
        // Check if the bullet's collider is overlapping with this specific addBullet's collider
        if (bulletCollider != null && bulletCollider.IsTouching(myCollider))
        {
            // trigger the sprite change
            ChangeSprite();
            // notify listeners of bullet loaded
            EventManager.bulletLoaded(true);
        }
    }


    public void ChangeSprite()
    {
        if (spriteRenderer != null && newSprite != null)
        {
            spriteRenderer.sprite = newSprite;
            Debug.Log("Sprite changed!");
        }
        else
        {
            Debug.LogWarning("SpriteRenderer or newSprite is not set!");
        }
    }

}
