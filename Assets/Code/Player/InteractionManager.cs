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
    private PCPlayer pCPlayer;
    private Transform grab;
    private bool canGrab;
    private bool canOpenDoor;
    private bool canOpenPC;
    private NPCRandom currentNPC;
    private bool canInteractWithNPC;

    private PlayerInteract playerInteract;
    private PlayerMovement playerMovement;

    public InteractionManager(float direction, Transform cam, GameObject crosshair, TextMeshProUGUI textMesh, PlayerInteract playerInteract, PlayerMovement playerMovement, Transform grab)
    {
        this.direction = direction;
        this.cam = cam;
        this.crosshair = crosshair;
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
            // Reset referensi NPC saat raycast dilakukan
            canInteractWithNPC = false;
            currentNPC = null;

            if (hit.collider.CompareTag("NPC"))
            {
                textMesh.text = "[E]";

                // Mendapatkan referensi ke NPCRandom script
                NPCRandom npc = hit.collider.GetComponent<NPCRandom>();
                if (npc != null)
                {
                    canInteractWithNPC = true;
                    currentNPC = npc; // Simpan referensi NPC
                }
            }

            canOpenPC = false;

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

    private bool npcInteractionInProgress = false; // Tambahkan variabel ini untuk melacak status interaksi dengan NPC

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

        // Interaksi dengan NPC
        if (canInteractWithNPC && currentNPC != null)
        {
            if (!npcInteractionInProgress)
            {
                // Interaksi pertama: NPC menghadap pemain dan pemain tidak bisa bergerak
                currentNPC.LookAtPlayer(cam);
                playerMovement.SetCanMove(false);
                npcInteractionInProgress = true; // Tandai bahwa interaksi sedang berlangsung
            }
            else
            {
                // Interaksi kedua: Mengembalikan kontrol pergerakan pemain dan memulai kembali aktivitas NPC secara acak
                playerMovement.SetCanMove(true);
                currentNPC.RandomAgain();
                npcInteractionInProgress = false; // Reset status interaksi
            }
        }

        //Interaksi dengan PC Player
        if (canOpenPC && pCPlayer != null)
        {
            pCPlayer.SetActive(true);
        }
    }


    public void SetPlayerInteract(PlayerInteract playerInteract)
    {
        this.playerInteract = playerInteract;
    }
}
