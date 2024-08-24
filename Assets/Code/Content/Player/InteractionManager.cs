using UnityEngine;
using TMPro;

public class InteractionManager
{
    private float direction;
    private Transform cam;
    private GameObject crosshair;
    private TextMeshProUGUI textMesh;
    private GrabObject currentGrabObject;
    private Door currentDoor;
    private Transform grab;
    private bool canGrab;
    private bool canOpenDoor;
    private PlayerInteract playerInteract;

    public InteractionManager(float direction, Transform cam, GameObject crosshair, TextMeshProUGUI textMesh, PlayerInteract playerInteract, Transform grab)
    {
        this.direction = direction;
        this.cam = cam;
        this.crosshair = crosshair;
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
        canOpenDoor = false;
        currentGrabObject = null;
        currentDoor = null;

        // Membuat ray dari titik tengah layar menggunakan arah kamera
        Ray ray = new Ray(cam.position, cam.forward);
        RaycastHit hit;

        // Menggambar ray untuk tujuan debugging
        Debug.DrawRay(ray.origin, ray.direction * direction, Color.red);

        // Interact dengan objek yang memiliki tag "GrabObject" atau "door"
        if (Physics.Raycast(ray, out hit, direction))
        {
            if (hit.collider.CompareTag("GrabObject"))
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
            if (hit.collider.CompareTag("door"))
            {
                Door door = hit.collider.GetComponent<Door>();

                if (door != null)
                {
                    textMesh.text = hit.collider.name;
                    canOpenDoor = true;
                    currentDoor = door;
                }
            }
            if (hit.collider.gameObject.name == "Button floor 1")
            {
                hit.transform.gameObject.GetComponent<pass_on_parent>().MyParent.GetComponent<evelator_controll>().AddTaskEve("Button floor 1");

            }
            if (hit.collider.gameObject.name == "Button floor 2")
            {
                hit.transform.gameObject.GetComponent<pass_on_parent>().MyParent.GetComponent<evelator_controll>().AddTaskEve("Button floor 2");
            }
            if (hit.collider.gameObject.name == "Button floor 3")
            {
                hit.transform.gameObject.GetComponent<pass_on_parent>().MyParent.GetComponent<evelator_controll>().AddTaskEve("Button floor 3");
            }
            if (hit.collider.gameObject.name == "Button floor 4")
            {
                hit.transform.gameObject.GetComponent<pass_on_parent>().MyParent.GetComponent<evelator_controll>().AddTaskEve("Button floor 4");
            }
            if (hit.collider.gameObject.name == "Button floor 5")
            {
                hit.transform.gameObject.GetComponent<pass_on_parent>().MyParent.GetComponent<evelator_controll>().AddTaskEve("Button floor 5");
            }
            if (hit.collider.gameObject.name == "Button floor 6")
            {
                hit.transform.gameObject.GetComponent<pass_on_parent>().MyParent.GetComponent<evelator_controll>().AddTaskEve("Button floor 6");
            }
        }
    }

    public void HandleInteract()
    {
        // Interaksi dengan objek yang bisa di-grab
        if (canGrab && currentGrabObject != null)
        {
            if (grab.childCount == 0)
            {
                currentGrabObject.ToggleGrab();
                if (currentGrabObject.isGrab)
                {
                    crosshair.SetActive(false);
                    textMesh.gameObject.SetActive(false);
                }
            }
            else
            {
                // Membuat local dari objek yang ada di dalam grab
                GrabObject getObjekInGrab = grab.GetChild(0).GetComponent<GrabObject>();
                getObjekInGrab.ToggleGrab();

                crosshair.SetActive(true);
                textMesh.gameObject.SetActive(true);
            }
        }

        // Interaksi dengan pintu
        if (canOpenDoor && currentDoor != null)
        {
            currentDoor.ActionDoor();
        }
    }

    public void SetPlayerInteract(PlayerInteract playerInteract)
    {
        this.playerInteract = playerInteract;
    }
}
