using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonController : MonoBehaviour
{
    // Access the sprite renderer
    private SpriteRenderer theSR;
    public Sprite defaultImage;
    public Sprite pressedImage;

    // what button to press
    public KeyCode keyToPress;
    // Start is called before the first frame update
    void Start()
    {
        theSR = GetComponent<SpriteRenderer>();


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(keyToPress))
        {
            // When key is pressed, make the sprite pressed
            theSR.sprite = pressedImage;
        }

        if (Input.GetKeyUp(keyToPress))
        {
            // Released key then change sprite
            theSR.sprite = defaultImage;
        }

    }
}
