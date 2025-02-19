using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class LeftHandActionManager : MonoBehaviour
{
    private Animator animator;
    private LeftHandActions leftHandActions;
    public bool canPickBullet = false;

    // Unity Events
    public UnityEvent onClickStart;
    public UnityEvent onClickEnd;
    private float vibrationFrequency = 28f;  // Speed of vibration
    private float baseAmplitude = 0.05f; // Intensity of vibration
    private float vibrationTime = 0f;
    private gameManager gm;
    private int currentScore;


    private void Awake()
    {
        // Initialize Animator
        animator = GetComponent<Animator>();

        // Initialize Input Actions
        leftHandActions = new LeftHandActions();
        gm = FindObjectOfType<gameManager>();
    }

    private void OnEnable()
    {
        // Enable Input Actions
        leftHandActions.Enable();

        // Subscribe to click events
        leftHandActions.Newactionmap.Click.started += OnClickStarted;
        leftHandActions.Newactionmap.Click.canceled += OnClickCanceled;
    }

    private void OnDisable()
    {
        // Unsubscribe from click events
        leftHandActions.Newactionmap.Click.started -= OnClickStarted;
        leftHandActions.Newactionmap.Click.canceled -= OnClickCanceled;

        // Disable Input Actions
        leftHandActions.Disable();
    }

    private void Update()
    {
        // Get mouse position and convert to world position
        Vector2 mousePos = leftHandActions.Newactionmap.MousePosition.ReadValue<Vector2>();
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, Camera.main.nearClipPlane));
        currentScore = gm.getScore();

        // Increase vibration amplitude based on score
        float currentAmplitude = baseAmplitude + (currentScore * 0.02f);

        // Vibration effect using sine wave
        vibrationTime += Time.deltaTime * vibrationFrequency;
        float vibrationOffsetX = Mathf.Sin(vibrationTime) * currentAmplitude;
        float vibrationOffsetY = Mathf.Cos(vibrationTime) * currentAmplitude;

        // Update object position with vibration effect
        transform.position = new Vector3(mouseWorldPos.x + vibrationOffsetX, mouseWorldPos.y + vibrationOffsetY, transform.position.z);
    }

    private void OnClickStarted(InputAction.CallbackContext context)
    {
        animator.SetBool("clicked", true);

        // observer pattern event

        //Debug.Log("triggering the on click start method");
        onClickStart.Invoke();
    }

    private void OnClickCanceled(InputAction.CallbackContext context)
    {
        animator.SetBool("clicked", false);

        //observer pattern mosue release
        onClickEnd.Invoke();
    }

}
