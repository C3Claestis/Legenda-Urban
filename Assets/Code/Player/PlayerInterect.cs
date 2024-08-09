using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerInteract : MonoBehaviour
{
    [Header("Jangkauan Raycast")][Range(0.1f, 5.0f)][SerializeField] private float direction = 2.5f;
    [Header("Layer Raycast Grab")][SerializeField] private LayerMask layer;
    [Header("Layer Raycast Interction")][SerializeField] private LayerMask layerInteract;
    [Header("Camera Referensi")][SerializeField] private Transform cam; // Referensi ke Kamera
    [Header("Crosshair Raycast")][SerializeField] private GameObject crosshair;
    [Header("Text Object Raycast")][SerializeField] private TextMeshProUGUI textMesh;
    [Header("Referensi Grab Raycast")] [SerializeField] Transform grab;
    private PlayerInputActions inputActions;
    private InteractionManager interactionManager;

    void Awake()
    {
        // Inisialisasi input actions
        inputActions = new PlayerInputActions();
        interactionManager = new InteractionManager(direction, layer, layerInteract, cam, crosshair, textMesh, this, grab);
        SetInteractionManager(interactionManager);
        interactionManager.SetPlayerInteract(this);
    }

    void OnEnable()
    {
        // Mengaktifkan input actions dan mengikat tindakan Interact
        inputActions.Player.Enable();
        inputActions.Player.Interact.performed += OnInteractPerformed;
    }

    void OnDisable()
    {
        // Menonaktifkan input actions
        inputActions.Player.Disable();
        inputActions.Player.Interact.performed -= OnInteractPerformed;
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

    private void OnInteractPerformed(InputAction.CallbackContext context)
    {
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
