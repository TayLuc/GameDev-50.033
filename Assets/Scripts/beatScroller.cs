using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatScroller : MonoBehaviour
{
    // Controls how fast the arrows drop
    public float beatTempo;
    // Check if game is start
    public bool hasStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        beatTempo /= 60f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasStarted)
        {
            //  handled by gameManager
            // if (Input.anyKeyDown)
            // {
            //     hasStarted = true;
            // }

        }
        else
        {
            // Standard beats in 120 bpm = 2 bps
            transform.position -= new Vector3(0f, beatTempo * Time.deltaTime, 0f);

        }

    }
}
