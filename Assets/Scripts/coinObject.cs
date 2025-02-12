using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinObject : MonoBehaviour
{
    public bool canBePressed;
    public KeyCode keyToPress;
    public GameObject hitEffect, goodEffect, perfectEffect, missEffect;
    private AudioClip coinSoundClip, fireBallSoundClip, kickSoundClip;
    public AudioSource coinSoundSource, fireBallSoundSource, kickSoundSource;

    // Start is called before the first frame update
    void Start()
    {
        if (coinSoundClip != null) coinSoundSource.clip = coinSoundClip;
        if (fireBallSoundClip != null) fireBallSoundSource.clip = fireBallSoundClip;
        if (kickSoundClip != null) kickSoundSource.clip = kickSoundClip;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(keyToPress))
        {
            if (canBePressed)
            {
                gameObject.SetActive(false);
                // update the gamemanager to say a note successfully hit
                // gameManager.instance.noteHit();
                // Check how far from zero the hit is 
                if (Mathf.Abs(transform.position.y) > 0.25) // Convert to positive value before comparing for lower check
                {
                    Debug.Log("Ok Hit");
                    gameManager.instance.normalHit();
                    Instantiate(hitEffect, transform.position, hitEffect.transform.rotation);
                    kickSoundSource.Play();

                }
                else if (Mathf.Abs(transform.position.y) > 0.05f)
                {
                    Debug.Log("Good Hit");
                    gameManager.instance.goodHit();
                    Instantiate(goodEffect, transform.position, goodEffect.transform.rotation);
                    fireBallSoundSource.Play();
                }
                else
                {
                    Debug.Log("Perfect Hit");
                    gameManager.instance.perfectHit();
                    Instantiate(perfectEffect, transform.position, perfectEffect.transform.rotation);
                    coinSoundSource.Play();
                }

            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Activator")
        {
            canBePressed = true;

        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Activator")
        {
            if (gameObject.activeSelf)  // Only mark it as missed if it's still active
            {
                canBePressed = false;
                gameManager.instance.noteMiss();
                Instantiate(missEffect, transform.position, missEffect.transform.rotation);
            }
        }
    }

}
