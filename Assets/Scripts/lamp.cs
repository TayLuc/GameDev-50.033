using UnityEngine;

public class Lamp : MonoBehaviour
{
    private Animator animator;
    private AudioSource audioSource;
    public AudioClip flickerSound;
    public float flickerInterval = 30f;

    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        InvokeRepeating(nameof(Flicker), 0f, flickerInterval);
    }

    void Flicker()
    {
        animator.SetTrigger("flicker");
    }

    // This function is triggered when the animation ends
    public void OnFlickerEnd()
    {
        animator.SetTrigger("returnIdle");
    }

    private void playFlickerSound()
    {
        if (flickerSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(flickerSound);
        }
    }
}
