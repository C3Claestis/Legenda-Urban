using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerInteract : MonoBehaviour
{
    [Range(0.1f, 20.0f)][SerializeField] private float direction = 10.0f;
    [SerializeField] private LayerMask layer;
    [SerializeField] private Transform cam; // Referensi ke Kamera
    [SerializeField] private GameObject crosshair;
    [SerializeField] private TextMeshProUGUI textMesh;
    public Transform grab;
    private PlayerInputActions inputActions;
    private InteractionManager interactionManager;

    void Awake()
    {
        // Inisialisasi input actions
        inputActions = new PlayerInputActions();
        interactionManager = new InteractionManager(direction, layer, cam, crosshair, textMesh, this);
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
