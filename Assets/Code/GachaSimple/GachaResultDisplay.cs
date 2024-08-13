using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class GachaResultDisplay : MonoBehaviour
{
    [Header("BASE OF GACHA DISPLAY")]
    [SerializeField] private WorldGachaManager worldGachaManager;
    [SerializeField] private InventoryGacha inventoryGacha;
    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private Image itemBackground;
    [SerializeField] private Animator animator;    
    
    [Header("BASE OF GACHA RESULT")]
    [SerializeField] Transform container;
    [SerializeField] GameObject itemTemplate;
    List<Image> background = new List<Image>();
    List<Image> icon = new List<Image>();
    List<TextMeshProUGUI> rating = new List<TextMeshProUGUI>();
    private void Start()
    {
        // Mengambil referensi dari child itemTemplate dan menambahkannya ke daftar
        if (itemTemplate != null)
        {
            // Mengambil child pertama (Image) dan child kedua (TextMeshProUGUI)
            Image backgroundTemplate = itemTemplate.GetComponent<Image>();
            Image templateImage = itemTemplate.transform.GetChild(0).GetComponent<Image>();
            TextMeshProUGUI templateText = itemTemplate.transform.GetChild(1).GetComponent<TextMeshProUGUI>();

            // Menambahkan referensi ke dalam daftar
            background.Add(backgroundTemplate);
            icon.Add(templateImage);
            rating.Add(templateText);
        }
    }
    public void DisplayGachaResults(List<GachaItem> gachaResults)
    {
        StartCoroutine(ShowGachaResults(gachaResults));
    }

    private IEnumerator ShowGachaResults(List<GachaItem> gachaResults)
    {
        yield return new WaitForSeconds(8f); // Tunggu 8 detik sebelum menampilkan hasil pertama

        foreach (var item in gachaResults)
        {
            // Update UI dengan hasil gacha saat ini
            itemIcon.sprite = item.image;
            itemName.text = item.itemName;
            itemBackground.color = item.color;

            // Mulai animasi
            animator.SetTrigger("ShowResult");

            AddItemToShowGacha(item.color, item.image, item.starRating.ToString()); // Add gacha result to show all gacha
            inventoryGacha.AddItemToInventory(item.color, item.image, item.starRating.ToString()); // Add gacha result to inventory
            
            // Tunggu sampai pengguna menekan tombol 'space' untuk melanjutkan ke hasil berikutnya
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));

            // Tunggu sampai animasi selesai sebelum menampilkan item berikutnya
            yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        }
        // Panggil metode untuk menandai bahwa seluruh hasil gacha telah ditampilkan
        OnGachaResultsComplete();
    }

    private void OnGachaResultsComplete()
    {
        worldGachaManager.ShowGacha();
    }

    public void AddItemToShowGacha(Color color, Sprite itemIcon, string itemText)
    {
        // Instantiate the template object to create a new inventory item
        GameObject newItem = Instantiate(itemTemplate, container);

        Image newBg = newItem.GetComponent<Image>();
        // Access the first child for the Image component
        Image newItemImage = newItem.transform.GetChild(0).GetComponent<Image>();

        // Access the second child for the TextMeshProUGUI component
        TextMeshProUGUI newItemRating = newItem.transform.GetChild(1).GetComponent<TextMeshProUGUI>();

        // Set the icon and text
        newBg.color = color;
        newItemImage.sprite = itemIcon;
        newItemRating.text = itemText;

        // Optionally, add this item to the list of inventory items for further reference
        background.Add(newBg);
        icon.Add(newItemImage);
        rating.Add(newItemRating);
    }    
}
