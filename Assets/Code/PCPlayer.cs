using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCPlayer : MonoBehaviour
{
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] GameObject panel_PC;
    [SerializeField] GameObject playerCamera, playerVirtualCamera;
    private bool isActive = false;
    public void SetActive(bool isActiving) => this.isActive = isActiving;
  
    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            panel_PC.SetActive(true);
            playerCamera.SetActive(false);
            playerVirtualCamera.SetActive(false);
            playerMovement.HandleCursor(false);
        }
    }

    public void Shutdown()
    {
        isActive = false;
        panel_PC.SetActive(false);
        playerCamera.SetActive(true);
        playerVirtualCamera.SetActive(true);
        playerMovement.HandleCursor(true);
    }
}
