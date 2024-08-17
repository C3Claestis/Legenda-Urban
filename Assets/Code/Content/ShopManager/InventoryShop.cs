using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryShop : MonoBehaviour
{
    [SerializeField] List<Image> inventory = new List<Image>();
    [SerializeField] Transform container;

    // Method to add an item to the inventory
    public void AddItemToInventory(Sprite itemIcon)
    {
        // Create a new UI Image object to display the item in the inventory
        GameObject newItem = new GameObject("InventoryItem");
        Image newItemImage = newItem.AddComponent<Image>();
        newItemImage.sprite = itemIcon;

        // Set the parent of the new item to the inventory container and adjust its position
        newItem.transform.SetParent(container, false);

        // Optionally, you can add this item to the list of inventory items for further reference
        inventory.Add(newItemImage);
    }
}
