using UnityEngine;
using TMPro;

public class InteractionManager
{
    private float direction;
    private LayerMask layer;
    private LayerMask layerInteract;
    private Transform cam;
    private GameObject crosshair;
    private TextMeshProUGUI textMesh;
    private GrabObject currentGrabObject;
    private Transform grab;
    private bool canGrab;
    private PlayerInteract playerInteract;

    public InteractionManager(float direction, LayerMask layer, LayerMask layerInteract, Transform cam, GameObject crosshair, TextMeshProUGUI textMesh, PlayerInteract playerInteract, Transform grab)
    {
        this.direction = direction;
        this.layer = layer;
        this.layerInteract = layerInteract;
        this.crosshair = crosshair;
        this.cam = cam;
        this.textMesh = textMesh;
        this.playerInteract = playerInteract;
        this.grab = grab;
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

        if (Physics.Raycast(ray, out hit, direction, layerInteract))
        {
            Transform NPCAction = hit.collider.GetComponent<Transform>();

            if (NPCAction != null)
            {
                if (Input.GetKey(KeyCode.O))
                {
                    Debug.Log("INTERKSI");
                    cam.gameObject.SetActive(false);
                    Transform shop = GameObject.Find("Shop").GetComponent<Transform>();
                    foreach (Transform child in shop.GetComponentInChildren<Transform>())
                    {
                        child.gameObject.SetActive(true);
                        Cursor.lockState = CursorLockMode.None;
                        Cursor.visible = true;
                    }
                }
            }
        }
    }

    public void HandleInteract()
    {
        if (grab.childCount == 0)
        {
            if (canGrab && currentGrabObject != null)
            {
                currentGrabObject.ToggleGrab();
                if (currentGrabObject.isGrab)
                {
                    crosshair.SetActive(false);
                    textMesh.gameObject.SetActive(false);
                }
            }
        }
        else
        {
            //Membuat local dari object yang ada didalam grab
            GrabObject getObjekInGrab = grab.GetChild(0).GetComponent<GrabObject>();
            getObjekInGrab.ToggleGrab();

            crosshair.SetActive(true);
            textMesh.gameObject.SetActive(true);
        }
    }

    public void SetPlayerInteract(PlayerInteract playerInteract)
    {
        this.playerInteract = playerInteract;
    }

}
