using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Transform cam;  // Kamera
    [SerializeField] Transform body; // Badan Pemain
    [SerializeField] CinemachineVirtualCamera virtualCamera; // Cinemachine virtual camera
    [Range(0f, 10.0f)] [SerializeField] float speed = 6.0f; // Speed move
    [Range(0f, 3.0f)] [SerializeField] float mouseSensitivity = 2.0f; // Mouse sens
    private float gravity = -9.81f;
    private CharacterController characterController;
    private Vector3 velocity;
    private PlayerInputActions inputActions;
    private Vector2 moveInput;
    private float xRotation = 0f;
    private CinemachinePOV povComponent;

    void Awake()
    {
        characterController = GetComponent<CharacterController>();

        // Initialize input actions
        inputActions = new PlayerInputActions();

        // Lock the cursor to the center of the screen and hide it
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Start()
    {
        // Get the POV component from the Cinemachine virtual camera
        povComponent = virtualCamera.GetCinemachineComponent<CinemachinePOV>();
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
        float mouseY = Mouse.current.delta.y.ReadValue() * mouseSensitivity * Time.deltaTime;

        // Update vertical camera rotation (pitch)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        cam.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Get the horizontal axis value from the Cinemachine POV component
        float yRotation = povComponent.m_HorizontalAxis.Value;
        body.rotation = Quaternion.Euler(0f, yRotation, 0f);
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
