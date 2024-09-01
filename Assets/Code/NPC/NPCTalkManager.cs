using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCTalkManager : MonoBehaviour
{
    [SerializeField] GameObject cameraSpeak;
    [SerializeField] NPCAction nPCAction;
    PlayerMovement playerMovement;
    Quaternion initialRotation;
    NPCRandom nPCRandom;
    GameObject virtualCam;
    GameObject mainCam;
    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.tag == "NPCRandom")
        {
            nPCRandom = GetComponent<NPCRandom>();
        }
        if (virtualCam == null)
        {
            virtualCam = GameObject.Find("Virtual Camera");
        }
        if (mainCam == null)
        {
            mainCam = GameObject.Find("Main Camera");
        }
        if (playerMovement == null)
        {
            playerMovement = FindObjectOfType<PlayerMovement>();
        }
       
        initialRotation = transform.rotation;
    }

    public void LookAtPlayer(Transform player)
    {
        if (gameObject.tag == "NPCRandom")
        {
            if (!nPCRandom.isNotRandom)
            {
                cameraSpeak.SetActive(true);
                virtualCam.SetActive(false);
                nPCAction.SetActive(false);
                // Hentikan NPC
                nPCRandom.agent.isStopped = true;
                nPCRandom.isNotRandom = true;
                // Rotasi NPC untuk menghadap ke pemain hanya di sumbu Y
                Vector3 direction = (player.position - transform.position).normalized;
                direction.y = 0; // Hanya sumbu Y yang diubah
                transform.rotation = Quaternion.LookRotation(direction);

                nPCRandom.ani.SetInteger("arms", 5);
                nPCRandom.ani.SetInteger("legs", 5);
            }
        }
        else
        {
            cameraSpeak.SetActive(true);
            virtualCam.SetActive(false);
            nPCAction.SetActive(false);
            // Rotasi NPC untuk menghadap ke pemain hanya di sumbu Y
            Vector3 direction = (player.position - transform.position).normalized;
            direction.y = 0; // Hanya sumbu Y yang diubah
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }
    public void RandomAgain()
    {
        if (gameObject.tag == "NPCRandom")
        {
            nPCRandom.isNotRandom = false;
            nPCRandom.agent.isStopped = false;
        }
        else
        {
            // Kembalikan NPC ke rotasi awal
            transform.rotation = initialRotation;
        }
        cameraSpeak.SetActive(false);
        virtualCam.SetActive(true);
        playerMovement.SetCanMove(true);
        nPCAction.SetActive(true);
    }
}
