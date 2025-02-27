using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    // Reference to your generated input actions class
    private CameraActions cameraActions;

    // Camera movement speed
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float leftBoundary = -20f;
    [SerializeField] private float rightBoundary = 4f;

    // Variable to store our horizontal movement value
    private float horizontalMovement;

    private void Awake()
    {
        // Initialize the input actions
        cameraActions = new CameraActions();
    }

    private void OnEnable()
    {
        // Enable the action map when this script is enabled
        cameraActions.Enable();

        // Subscribe to the camera move action using the Gameplay action map
        cameraActions.Gameplay.CameraMove.performed += OnCameraMove;
        cameraActions.Gameplay.CameraMove.canceled += OnCameraMove;
    }

    private void OnDisable()
    {
        // Unsubscribe from the action when this script is disabled
        cameraActions.Gameplay.CameraMove.performed -= OnCameraMove;
        cameraActions.Gameplay.CameraMove.canceled -= OnCameraMove;

        // Disable the action map
        cameraActions.Disable();
    }

    // Called when the CameraMove input changes
    private void OnCameraMove(InputAction.CallbackContext context)
    {
        // Read the input value (-1 for A, 1 for D, 0 for neither/both)
        horizontalMovement = context.ReadValue<float>();

        // Optional debug to verify input is working
        Debug.Log($"Camera movement value: {horizontalMovement}");
    }

    private void Update()
    {
        // Calculate the potential new position
        Vector3 movement = new Vector3(horizontalMovement, 0, 0) * moveSpeed * Time.deltaTime;
        Vector3 newPosition = transform.position + movement;

        // Clamp the X position to stay within boundaries
        newPosition.x = Mathf.Clamp(newPosition.x, leftBoundary, rightBoundary);

        // Apply the clamped position
        transform.position = newPosition;
    }
}