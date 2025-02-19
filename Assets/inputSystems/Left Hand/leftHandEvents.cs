using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class leftHandEvents : MonoBehaviour
{

    public GameObject bulletPrefab;
    public Transform spawnPoint;
    public GameObject ammoBox;
    public GameObject leftHand;
    private GameObject spawnedBullet;


    public void OnEnable()
    {
        EventManager.OnBulletLoaded += deleteBullet;
    }
    public void PickUpAmmo()
    {
        // ammo spawn 
        Debug.Log("clicked down");
        BoxCollider2D ammoboxCollider = ammoBox.GetComponent<BoxCollider2D>();
        BoxCollider2D leftHandCollider = leftHand.GetComponent<BoxCollider2D>();

        if (bulletPrefab != null && spawnPoint != null && ammoboxCollider.IsTouching(leftHandCollider))
        {
            spawnedBullet = Instantiate(bulletPrefab, spawnPoint.position, spawnPoint.rotation); // Spawn the bullet
            spawnedBullet.transform.parent = leftHand.transform; // Attach to leftHand
            Debug.Log("Picked up ammo and spawned bullet!");
        }
    }


    public void DropAmmo()
    {
        if (spawnedBullet != null)
        {
            // Notify all listeners
            CircleCollider2D bulletCollider = spawnedBullet.GetComponent<CircleCollider2D>();
            if (bulletCollider != null)
            {
                // Notify all addBullet scripts that a bullet has been dropped
                Debug.Log("notifying ammo listeners");
                EventManager.BulletDropped(bulletCollider);
            }


            if (spawnedBullet != null) // check again cuz the bullet might have been destroyed
            {
                //destroy the bullet by falling down
                spawnedBullet.transform.parent = null; // Detach
                Rigidbody2D rb = spawnedBullet.GetComponent<Rigidbody2D>(); // Get existing Rigidbody2D
                if (rb != null)
                {
                    rb.bodyType = RigidbodyType2D.Dynamic; // Change to dynamic
                }

                Destroy(spawnedBullet, 2f); // Destroy the bullet
                spawnedBullet = null;
                Debug.Log("Dropped ammo and destroyed bullet!");
            }

        }
        else
        {
            Debug.Log("No bullet to drop!");
        }
    }

    private void deleteBullet(bool loaded)
    {
        if (spawnedBullet != null && loaded)
        {
            Destroy(spawnedBullet);
            spawnedBullet = null;
            Debug.Log("instant bullet destruction");
        }
    }
}
