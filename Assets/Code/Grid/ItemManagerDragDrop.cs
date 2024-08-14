using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ItemManagerDragDrop : MonoBehaviour
{
    [SerializeField] List<ItemObject> inventory = new List<ItemObject>();
    [SerializeField] Transform container;
    [SerializeField] GameObject itemTemplate;
    List<Image> icon = new List<Image>();

    void Start()
    {
        // Mengambil referensi dari child itemTemplate dan menambahkannya ke daftar
        if (itemTemplate != null)
        {
            // Mengambil child pertama (Image) dan child kedua (TextMeshProUGUI)
            Image templateImage = itemTemplate.transform.GetChild(0).GetComponent<Image>();

            // Menambahkan referensi ke dalam daftar
            icon.Add(templateImage);
        }
        // Menambahkan 10 item acak ke panel
        for (int i = 0; i < 10; i++)
        {
            AddRandomItemToPanel();
        }
    }
    // Method to add a random item from the inventory to the panel
    private void AddRandomItemToPanel()
    {
        // Memeriksa apakah inventory memiliki item
        if (inventory.Count == 0)
        {
            Debug.LogWarning("Inventory is empty. No items to add.");
            return;
        }

        // Mengambil item secara acak dari inventory
        int randomIndex = Random.Range(0, inventory.Count);
        ItemObject randomItem = inventory[randomIndex];

        // Menambahkan item ke panel
        AddItemToPanel(randomItem);
    }
    // Method to add an item to the inventory using ItemObject
    public void AddItemToPanel(ItemObject itemObject)
    {
        // Create a new UI Image object to display the item in the inventory
        GameObject newItem = Instantiate(itemTemplate, container);

        // Access the first child for the Image component
        Image newItemImage = newItem.transform.GetChild(0).GetComponent<Image>();

        // Set the sprite of the new item to the item icon from ItemObject
        newItemImage.sprite = itemObject.icon;

        // Set the parent of the new item to the inventory container and adjust its position
        newItem.transform.SetParent(container, false);

        // Optionally, you can add this item to the list of inventory items for further reference
        icon.Add(newItemImage);

        // Instantiate the GameObject from ItemObject and set it as child of the newItem
        GameObject itemPrefab = Instantiate(itemObject.prefab, newItem.transform);

        // Adjust the position and other properties of the itemPrefab if necessary
        itemPrefab.transform.localPosition = new Vector3(0f, 0.5f, 0f);
        itemPrefab.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

        // Optionally, you can add this itemPrefab to the inventory list if needed
        // inventory.Add(itemObject);
    }
}