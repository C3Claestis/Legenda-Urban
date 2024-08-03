using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed = 6.0f;
    [SerializeField] Transform cam;  // Kamera
    [SerializeField] Transform body; // Badan Pemain
    [SerializeField] float mouseSensitivity = 2.0f;
    private float gravity = -9.81f;
    private CharacterController characterController;

    private Vector3 velocity;
    private PlayerInputActions inputActions;
    private Vector2 moveInput;
    private float xRotation = 0f;

    void Awake()
    {
        characterController = GetComponent<CharacterController>();

        // Initialize input actions
        inputActions = new PlayerInputActions();

        // Lock the cursor to the center of the screen and hide it
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void OnEnable()
    {
        // Enable the input actions
        inputActions.Player.Enable();
        inputActions.Player.Move.performed += OnMovePerformed;
        inputActions.Player.Move.canceled += OnMoveCanceled;
    }

    void OnDisable()
    {
        // Disable the input actions
        inputActions.Player.Move.performed -= OnMovePerformed;
        inputActions.Player.Move.canceled -= OnMoveCanceled;
        inputActions.Player.Disable();
    }

    void Update()
    {
        // Calculate movement direction
        Vector3 move = CalculateMovementDirection(moveInput.x, moveInput.y);

        // Apply gravity
        if (characterController.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        velocity.y += gravity * Time.deltaTime;

        // Apply movement and gravity
        characterController.Move((move * speed + velocity) * Time.deltaTime);

        // Handle camera and body rotation
        HandleCameraRotation();
    }

    Vector3 CalculateMovementDirection(float moveHorizontal, float moveVertical)
    {
        // Calculate camera's forward and right directions
        Vector3 camForward = cam.forward;
        Vector3 camRight = cam.right;

        // Flatten the camera's forward vector (ignore the y component)
        camForward.y = 0f;
        camForward.Normalize();
        camRight.y = 0f;
        camRight.Normalize();

        // Calculate the desired move direction relative to the camera
        Vector3 move = camForward * moveVertical + camRight * moveHorizontal;

        return move.normalized;
    }

    void HandleCameraRotation()
    {
        // Get mouse input for camera rotation
        float mouseX = Mouse.current.delta.x.ReadValue() * mouseSensitivity * Time.deltaTime;
        float mouseY = Mouse.current.delta.y.ReadValue() * mouseSensitivity * Time.deltaTime;

        // Update vertical camera rotation (pitch)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        cam.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Update horizontal body rotation (yaw)
        float yRotation = cam.eulerAngles.y;
        body.rotation = Quaternion.Euler(0f, yRotation, 0f);

        // Debug logs to check values
        Debug.Log($"Camera Y Rotation: {yRotation}");
        Debug.Log($"Body Rotation: {body.rotation.eulerAngles}");
    }

    void OnMovePerformed(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    void OnMoveCanceled(InputAction.CallbackContext context)
    {
        moveInput = Vector2.zero;
    }
}
