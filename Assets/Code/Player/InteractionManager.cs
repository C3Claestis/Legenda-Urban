using UnityEngine;
using TMPro;

public class InteractionManager
{
    private float direction;
    private LayerMask layer;
    private Transform cam;
    private GameObject crosshair;
    private TextMeshProUGUI textMesh;
    private GrabObject currentGrabObject;
    private bool canGrab;
    private PlayerInteract playerInteract;

    public InteractionManager(float direction, LayerMask layer, Transform cam, GameObject crosshair, TextMeshProUGUI textMesh, PlayerInteract playerInteract)
    {
        this.direction = direction;
        this.layer = layer;
        this.crosshair = crosshair;
        this.cam = cam;
        this.textMesh = textMesh;
        this.playerInteract = playerInteract;
    }

    public void PerformRaycast()
    {
        if (cam == null)
        {
            Debug.LogError("Referensi kamera tidak diatur.");
            return;
        }

        textMesh.text = string.Empty;
        canGrab = false;
        currentGrabObject = null;

        // Membuat ray dari titik tengah layar menggunakan arah kamera
        Ray ray = new Ray(cam.position, cam.forward);
        RaycastHit hit;

        // Menggambar ray untuk tujuan debugging
        Debug.DrawRay(ray.origin, ray.direction * direction, Color.red);

        if (Physics.Raycast(ray, out hit, direction, layer))
        {
            GrabObject grabObject = hit.collider.GetComponent<GrabObject>();
            if (grabObject != null)
            {
                textMesh.text = hit.collider.name;
                canGrab = true;
                currentGrabObject = grabObject;
                grabObject.SetIsCanGrab(true);

                // Mengatur referensi grabObject dengan transform grab dari PlayerInteract
                playerInteract.SetGrabReference(grabObject);
            }
        }
    }

    public void HandleInteract()
    {
        if (canGrab && currentGrabObject != null)
        {
            currentGrabObject.ToggleGrab();
            if (currentGrabObject.isGrab)
            {
                crosshair.SetActive(false);
                textMesh.gameObject.SetActive(false);
            }
            else
            {
                crosshair.SetActive(true);
                textMesh.gameObject.SetActive(true);
            }
        }
    }
    public void SetPlayerInteract(PlayerInteract playerInteract)
    {
        this.playerInteract = playerInteract;
    }

}
