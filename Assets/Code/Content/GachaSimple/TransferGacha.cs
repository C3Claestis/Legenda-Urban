using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TransferGacha : MonoBehaviour
{
    [SerializeField] WorldGachaManager gachaManager;
    [SerializeField] string Type;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && Type == "Limited")
        {
            gachaManager.SetLimited(+50);
        }
        else if (other.CompareTag("Player") && Type == "Weapon")
        {
            gachaManager.SetWeapon(+50);
        }
        else if (other.CompareTag("Player") && Type == "Standard")
        {
            gachaManager.SetStandard(+50);
        }
    }
}
