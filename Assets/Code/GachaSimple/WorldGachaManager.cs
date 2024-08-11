using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class WorldGachaManager : MonoBehaviour
{
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
        
    }
}
