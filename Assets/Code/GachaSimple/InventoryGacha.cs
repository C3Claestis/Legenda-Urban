using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryGacha : MonoBehaviour
{
    List<Image> background = new List<Image>();
    List<Image> icon = new List<Image>();
    List<TextMeshProUGUI> rating = new List<TextMeshProUGUI>();
    [SerializeField] Transform container;
    [SerializeField] GameObject itemTemplate; // Referensi objek template

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

    // Method to add an item to the inventory
    public void AddItemToInventory(Color color, Sprite itemIcon, string itemText)
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
