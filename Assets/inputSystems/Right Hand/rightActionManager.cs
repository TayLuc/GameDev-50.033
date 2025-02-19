using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class rightActionManager : MonoBehaviour, RightHandActions.IGameplayActions
{
    private RightHandActions rightHandActions;
    private Vector2 moveInput = Vector2.zero; // Ensures default state
    private bool isAnimating = false;
    public float moveSpeed = 5f; // Movement speed

    public UnityEvent rotateRevolver;
    public UnityEvent spinAndLoad;
    public UnityEvent fireGun;
    private Animator animator;
    private bool canFire;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        if (rightHandActions == null) // Ensure only one instance is created
        {
            rightHandActions = new RightHandActions();

        }
    }

    private void Start()
    {


    }

    private void OnEnable()
    {
        rightHandActions.Gameplay.SetCallbacks(this);
        rightHandActions.Gameplay.Enable();
        EventManager.OnBulletLoaded += HandleBulletLoaded;
        EventManager.OnSpinFinished += pointAtHead; // event listener
    }

    private void OnDisable()
    {
        rightHandActions.Gameplay.Disable();
        EventManager.OnSpinFinished -= pointAtHead;
    }

    private void Update()
    {
        if (!isAnimating)
        {
            // Move smoothly based on input
            Vector3 moveDirection = (transform.right * moveInput.x + transform.forward * moveInput.y).normalized;
            transform.position += moveDirection * moveSpeed * Time.deltaTime;
        }

    }

    public void OnHandMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>(); // Read Vector2 input for 3D movement
        Debug.Log($"Move Input: {moveInput}");

    }


    public void OnGunOpen(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Gun Open triggered");
            animator.SetTrigger("TurnRevolver");
            isAnimating = true;

            rotateRevolver.Invoke();
            // Start a coroutine to reset the flag after the animation is finished
            StartCoroutine(ResetAnimationFlag(2f));
        }
    }

    public void OnShootGun(InputAction.CallbackContext context)
    {
        if (context.started)
        {

            if (canFire)
            {
                Debug.Log("firing gun");
                fireGun.Invoke();
            }
            else
            {
                Debug.Log("cannot fire gun");
            }
        }
        canFire = false;// expend the bullet
    }

    private void HandleBulletLoaded(bool loaded)
    {
        if (loaded)
        {
            spinAndLoad.Invoke();
        }

    }

    private void pointAtHead(bool spinDone)
    {
        if (spinDone)
        {
            animator.SetTrigger("FireTrigger");
            canFire = true;
        }

    }

    private IEnumerator ResetAnimationFlag(float delay)
    {
        // Wait for the animation duration to complete
        yield return new WaitForSeconds(delay);

        // Reset the flag and allow movement
        isAnimating = false;
    }



}
