using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveLeftHand : MonoBehaviour
{
    private Vector3 offset;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Update clicked status based on mouse input
        if (Input.GetMouseButtonDown(0)) // Mouse button down (left-click)
        {
            animator.SetBool("clicked", true);
        }
        else if (Input.GetMouseButtonUp(0)) // Mouse button up (left-click released)
        {
            animator.SetBool("clicked", false);
        }

        // Make the sprite follow the mouse if clicked

        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mouseWorldPos.x, mouseWorldPos.y, transform.position.z);

    }
}
