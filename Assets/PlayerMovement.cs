using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed = 6.0f;
    [SerializeField] Transform cam;
    private float gravity = -9.81f;
    private float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;
    private CharacterController characterController;
    private Animator animator;
    private Vector3 velocity;
    private PlayerInputActions inputActions;
    private Vector2 moveInput;

    void Awake()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();

        // Initialize input actions
        inputActions = new PlayerInputActions();

        // Mengunci cursor ke tengah layar dan membuatnya tidak terlihat
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
        // Calculate the movement direction
        Vector3 move = CalculateMovementDirection(moveInput.x, moveInput.y);

        // Apply gravity
        if (characterController.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        // Apply gravity if not grounded
        velocity.y += gravity * Time.deltaTime;

        // Apply movement and gravity
        characterController.Move((move * speed + velocity) * Time.deltaTime);

        // Handle animation
        AnimationHandle(move);
    }

    Vector3 CalculateMovementDirection(float moveHorizontal, float moveVertical)
    {
        // Calculate the desired move direction
        Vector3 move = new Vector3(moveHorizontal, 0, moveVertical).normalized;

        if (move.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(move.x, move.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            return moveDir.normalized;
        }

        return Vector3.zero;
    }
    void AnimationHandle(Vector3 move)
    {
        // Check if the player is moving
        bool isMoving = move.magnitude > 0;

        // Set the "IsWalking" parameter in the Animator to the value of isMoving
        animator.SetBool("IsWalking", isMoving);
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