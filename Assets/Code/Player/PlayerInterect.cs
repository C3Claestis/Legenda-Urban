using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerInteract : MonoBehaviour
{
    [Header("Player Movement Referensi")][SerializeField] private PlayerMovement playerMovement; // Referensi ke Playermove
    [Header("Jangkauan Raycast")][Range(0.1f, 5.0f)][SerializeField] private float direction = 2.5f;
    [Header("Camera Referensi")][SerializeField] private Transform cam; // Referensi ke Kamera
    [Header("Text Object Raycast")][SerializeField] private TextMeshProUGUI textMesh;
    [Header("Referensi Grab Raycast")] [SerializeField] Transform grab;
    private PlayerInputActions inputActions;
    private InteractionManager interactionManager;

    void Awake()
    {
        // Inisialisasi input actions
        inputActions = new PlayerInputActions();
        interactionManager = new InteractionManager(direction, cam, textMesh, this, playerMovement, grab);
        SetInteractionManager(interactionManager);
        interactionManager.SetPlayerInteract(this);
    }

    void OnEnable()
    {
        // Mengaktifkan input actions dan mengikat tindakan Interact
        inputActions.Player.Enable();
        inputActions.Player.Interact.performed += OnInteractPerformed;
        inputActions.Player.Object.performed += OnInteractObject;
    }

    void OnDisable()
    {
        // Menonaktifkan input actions
        inputActions.Player.Disable();
        inputActions.Player.Interact.performed -= OnInteractPerformed;
        inputActions.Player.Object.performed -= OnInteractObject;
    }

    void Start()
    {
        if (cam == null)
        {
            cam = Camera.main.transform;
        }
    }

    void Update()
    {
        interactionManager.PerformRaycast();
    }

    //Interaksi untuk object grab saja dengan LEFT_MOUSE
    private void OnInteractObject(InputAction.CallbackContext context)
    {
        // Handle additional interaction
        interactionManager.HandleInteractObject();
    }
    //Interaksi untuk environment dan NPC dengan E Keyboard
    private void OnInteractPerformed(InputAction.CallbackContext context)
    {
        // Handle additional interaction
        interactionManager.HandleInteract();
    }

    public void SetGrabReference(GrabObject grabObject)
    {
        grabObject.SetReference(grab);
    }
    public void SetInteractionManager(InteractionManager interactionManager)
    {
        this.interactionManager = interactionManager;
    }
}
