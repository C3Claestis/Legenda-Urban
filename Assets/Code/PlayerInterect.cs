using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    [Range(0.1f, 20.0f)] [SerializeField] float direction;
    [SerializeField] LayerMask layer;
    [SerializeField] Transform cam; // Reference to the Camera
    private PlayerInputActions inputActions;

    void Awake()
    {
        // Initialize input actions
        inputActions = new PlayerInputActions();
    }

    void OnEnable()
    {
        // Enable the input actions and bind the Interact action
        inputActions.Player.Enable();
    }

    void OnDisable()
    {
        // Disable the input actions
        inputActions.Player.Disable();
    }

    void Update()
    {
        PerformRaycast();
    }

    private void PerformRaycast()
    {
        if (cam == null)
        {
            Debug.LogError("Camera reference is not set.");
            return;
        }

        Ray ray = cam.GetComponent<Camera>().ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;

        // Draw the ray for debugging purposes
        Debug.DrawRay(ray.origin, ray.direction * direction, Color.red);

        if (Physics.Raycast(ray, out hit, direction, layer))
        {
            Debug.Log("Hit: " + hit.collider.name);

            // Perform interaction logic here
        }
    }
}
