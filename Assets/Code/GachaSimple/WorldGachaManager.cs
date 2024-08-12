using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class WorldGachaManager : MonoBehaviour
{
    [SerializeField] GameObject PanelInventory;
    [SerializeField] GameObject gachasystem;
    [SerializeField] GameObject cameraPlayer;
    [SerializeField] GameObject PlayerBody;
    [SerializeField] TextMeshProUGUI LimitedUIGacha, LimitedUI;
    [SerializeField] TextMeshProUGUI WeaponUIGacha, WeaponUI;
    [SerializeField] TextMeshProUGUI StandardUIGacha, StandardUI;
    private int LimitedCount;
    private int WeaponCount;
    private int StandardCount;
    public void SetLimited(int count) => LimitedCount = count;
    public void SetWeapon(int count) => WeaponCount = count;
    public void SetStandard(int count) => StandardCount = count;
    public GameObject Gachasystem => gachasystem;
    public GameObject CameraPlayer => cameraPlayer;
    public GameObject Player => PlayerBody;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            PanelInventory.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            PanelInventory.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            gachasystem.SetActive(false);
            cameraPlayer.SetActive(true);
            PlayerBody.SetActive(true);

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    
        LimitedUI.text = LimitedCount.ToString();
        LimitedUIGacha.text = LimitedCount.ToString();

        WeaponUI.text = WeaponCount.ToString();
        WeaponUIGacha.text = WeaponCount.ToString();

        StandardUI.text = StandardCount.ToString();
        StandardUIGacha.text = StandardCount.ToString();
    }
}
