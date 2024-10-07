using UnityEngine;
using TMPro;

public class InteractionManager
{
    private float direction;
    private Transform cam;
    private TextMeshProUGUI textMesh;
    private GrabObject currentGrabObject;
    private Door currentDoor;
    private PCPlayer pCPlayer;
    private Transform grab;
    private bool canGrab;
    private bool canOpenDoor;
    private bool canOpenPC;
    private NPCTalkManager currentNPC;
    private bool canInteractWithNPC;

    private PlayerInteract playerInteract;
    private PlayerMovement playerMovement;

    public InteractionManager(float direction, Transform cam, TextMeshProUGUI textMesh, PlayerInteract playerInteract, PlayerMovement playerMovement, Transform grab)
    {
        this.direction = direction;
        this.cam = cam;
        this.textMesh = textMesh;
        this.playerInteract = playerInteract;
        this.playerMovement = playerMovement;
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

        // Reset referensi NPC sebelum raycast dilakukan
        canInteractWithNPC = false;
        currentNPC = null;

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
                    textMesh.text = "[E]";
                    canOpenDoor = true;
                    currentDoor = door;
                }
            }

            if (hit.collider.CompareTag("NPCRandom") || hit.collider.CompareTag("NPC"))
            {
                textMesh.text = "[E]";

                // Mendapatkan referensi ke NPCTalkManager script
                NPCTalkManager npc = hit.collider.GetComponent<NPCTalkManager>();
                if (npc != null)
                {
                    canInteractWithNPC = true;
                    currentNPC = npc; // Simpan referensi NPC
                }
            }

            if (hit.collider.gameObject.name == "PC-Player")
            {
                textMesh.text = "[E]";

                PCPlayer pCPlayerplayer = hit.collider.GetComponent<PCPlayer>();

                if (pCPlayerplayer != null)
                {
                    canOpenPC = true;
                    pCPlayer = pCPlayerplayer;
                }
            }
        }
    }

    public void HandleInteract()
    {
        // Interaksi dengan pintu
        if (canOpenDoor && currentDoor != null)
        {
            currentDoor.ActionDoor();
        }

        // Interaksi dengan NPC Random
        if (canInteractWithNPC && currentNPC != null && playerMovement.GetCanMove())
        {
            currentNPC.LookAtPlayer(cam);
            playerMovement.SetCanMove(false);

            // Reset interaksi NPC setelah berinteraksi
            canInteractWithNPC = false;
            currentNPC = null;
        }

        //Interaksi dengan PC Player
        if (canOpenPC && pCPlayer != null)
        {
            pCPlayer.SetActive(true);
        }
    }
    public void HandleInteractObject()
    {
        // Interaksi dengan objek yang bisa di-grab
        if (canGrab && currentGrabObject != null)
        {
            // Jika tidak ada objek di grab, grab objek baru
            if (grab.childCount == 0)
            {
                currentGrabObject.SetReference(grab); // Mengatur referensi grab
                currentGrabObject.ToggleGrab(); // Mengaktifkan grab
            }
        }

        if (!canGrab && currentGrabObject == null && grab.childCount != 0)
        {
            // Jika ada objek di grab, lepaskan objek tersebut
            GrabObject getObjekInGrab = grab.GetChild(0).GetComponent<GrabObject>();
            getObjekInGrab.ToggleGrab(); // Melepas grab
            getObjekInGrab.SetReference(null); // Menghapus referensi grab

            // Jika objek yang sedang di-grab sama dengan currentGrabObject, reset currentGrabObject
            if (getObjekInGrab == currentGrabObject)
            {
                currentGrabObject = null;
            }
        }
    }
    public void SetPlayerInteract(PlayerInteract playerInteract)
    {
        this.playerInteract = playerInteract;
    }
}
