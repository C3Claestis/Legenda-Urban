using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class WorldGachaManager : MonoBehaviour
{
    [SerializeField] GachaSystem gachaSystem;
    [SerializeField] GachaResultDisplay gachaResultDisplay;
    [SerializeField] GameObject PanelInventory;
    [SerializeField] GameObject gachaBanner;
    [SerializeField] GameObject gachaSystemObject;
    [SerializeField] GameObject PanelShowGacha;
    [SerializeField] Transform containerShowGacha;
    [SerializeField] GameObject cameraPlayer;
    [SerializeField] GameObject PlayerBody;
    [SerializeField] TextMeshProUGUI LimitedUIGacha, LimitedUI;
    [SerializeField] TextMeshProUGUI WeaponUIGacha, WeaponUI;
    [SerializeField] TextMeshProUGUI StandardUIGacha, StandardUI;
    [SerializeField] RectTransform uiElementToMove;
    private int LimitedCount;
    private int WeaponCount;
    private int StandardCount;
    public void SetLimited(int count) => LimitedCount = count;
    public void SetWeapon(int count) => WeaponCount = count;
    public void SetStandard(int count) => StandardCount = count;
    public GameObject GachaBanner => gachaBanner;
    public GameObject CameraPlayer => cameraPlayer;
    public GameObject Player => PlayerBody;

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
            gachaBanner.SetActive(false);
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

    //Limited Character Gacha
    public void SingleGachaLimited()
    {
        if (LimitedCount >= 1)
        {
            LimitedCount--;
            StartGacha();
            var result = gachaSystem.PullLimitedCharacter(1);
            gachaResultDisplay.DisplayGachaResults(new List<GachaItem> { result });

        }
    }
    public void MultipleGachaLimited()
    {
        if (LimitedCount >= 10)
        {
            LimitedCount -= 10;
            StartGacha();
            var results = new List<GachaItem>();
            for (int i = 0; i < 10; i++)
            {
                results.Add(gachaSystem.PullLimitedCharacter(1));
            }
            gachaResultDisplay.DisplayGachaResults(results);
        }
    }

    //Weapon Gacha
    public void SingleGachaWeapon()
    {
        if (WeaponCount >= 1)
        {
            WeaponCount--;
            StartGacha();
            var result = gachaSystem.PullLimitedWeapon(1);
            gachaResultDisplay.DisplayGachaResults(new List<GachaItem> { result });
        }
    }
    public void MultipleGachaWeapon()
    {
        if (WeaponCount >= 10)
        {
            WeaponCount -= 10;
            StartGacha();
            var results = new List<GachaItem>();
            for (int i = 0; i < 10; i++)
            {
                results.Add(gachaSystem.PullLimitedWeapon(1));
            }
            gachaResultDisplay.DisplayGachaResults(results);
        }
    }

    //Standard Gacha
    public void SingleGachaStandard()
    {
        if (StandardCount >= 1)
        {
            StandardCount--;
            StartGacha();
            var result = gachaSystem.PullStandard(1);
            Debug.Log("Hasil Gacha: " + result.itemName);
            gachaResultDisplay.DisplayGachaResults(new List<GachaItem> { result });
        }
    }
    public void MultipleGachaStandard()
    {
        if (StandardCount >= 10)
        {
            StandardCount -= 10;
            StartGacha();
            var results = new List<GachaItem>();
            for (int i = 0; i < 10; i++)
            {
                results.Add(gachaSystem.PullStandard(1));
            }
            gachaResultDisplay.DisplayGachaResults(results);
        }
    }

    void StartGacha()
    {
        // Mengubah posisi RectTransform objek UI
        if (uiElementToMove != null)
        {
            RectTransform rectTransform = uiElementToMove;
            Vector2 newAnchoredPosition = new Vector2(rectTransform.anchoredPosition.x, -1000f);
            rectTransform.anchoredPosition = newAnchoredPosition;
        }
        gachaBanner.SetActive(false);
        gachaSystemObject.SetActive(true);
    }

    public void ShowGacha()
    {
        PanelShowGacha.SetActive(true);
    }
    public void FinishGacha()
    {
        for (int i = containerShowGacha.childCount - 1; i >= 0; i--)
        {
            Destroy(containerShowGacha.GetChild(i).gameObject);
        }

        PanelShowGacha.SetActive(false);
        PlayerBody.SetActive(true);
        cameraPlayer.SetActive(true);
        gachaSystemObject.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
