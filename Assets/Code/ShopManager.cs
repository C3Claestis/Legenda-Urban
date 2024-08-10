using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class ShopManager : MonoBehaviour
{
    [SerializeField] List<ItemShop> availableItems;  // Daftar item yang tersedia di shop
    [SerializeField] InventoryShop inventoryShop;
    [SerializeField] GameObject itemPrefab;          // Prefab UI untuk item di shop
    [SerializeField] Transform itemContainer;        // Tempat item-item di shop ditampilkan
    [SerializeField] TextMeshProUGUI currencyText;   // UI text untuk menampilkan saldo mata uang
    [SerializeField] GameObject confirmationPanel;   // Panel konfirmasi
    [SerializeField] TextMeshProUGUI confirmationItemName; // Teks untuk nama item di konfirmasi
    [SerializeField] TextMeshProUGUI confirmationItemPrice; // Teks untuk harga item di konfirmasi
    [SerializeField] Image confirmationItemIcon;     // Gambar untuk ikon item di konfirmasi
    [SerializeField] Button confirmButton;           // Tombol konfirmasi pembelian
    private int playerCurrency = 999;     // Saldo mata uang player
    private ItemShop selectedItem;         // Item yang sedang dipilih

    [SerializeField] GameObject shopPanel, cameraShop, virtualCamera;
    void Start()
    {
        UpdateCurrencyUI();
        PopulateShop();
        confirmationPanel.SetActive(false); // Sembunyikan panel konfirmasi saat start
    }

    // Mengisi shop dengan item yang tersedia
    void PopulateShop()
    {
        foreach (ItemShop item in availableItems)
        {
            GameObject newItem = Instantiate(itemPrefab, itemContainer);
            newItem.transform.Find("ItemName").GetComponent<TextMeshProUGUI>().text = item.itemName;
            newItem.transform.Find("ItemPrice").GetComponent<TextMeshProUGUI>().text = item.itemPrice.ToString();
            newItem.transform.Find("ItemIcon").GetComponent<Image>().sprite = item.itemIcon;
            newItem.transform.Find("GoldIcon").GetComponent<Image>().sprite = item.goldIcon;

            Button itemButton = newItem.GetComponent<Button>();
            itemButton.onClick.AddListener(() => OpenConfirmationPanel(item));
        }
    }

    // Membuka panel konfirmasi
    void OpenConfirmationPanel(ItemShop item)
    {
        selectedItem = item;
        confirmationItemName.text = item.itemName;
        confirmationItemPrice.text = "Price: " + item.itemPrice.ToString();
        confirmationItemIcon.sprite = item.itemIcon;
        confirmationPanel.SetActive(true);

        confirmButton.onClick.RemoveAllListeners(); // Hapus listener sebelumnya
        confirmButton.onClick.AddListener(ConfirmPurchase);
    }

    // Mengonfirmasi pembelian
    void ConfirmPurchase()
    {
        TryPurchaseItem(selectedItem);
        confirmationPanel.SetActive(false); // Sembunyikan panel konfirmasi setelah pembelian
    }

    // Coba beli item
    void TryPurchaseItem(ItemShop item)
    {
        if (playerCurrency >= item.itemPrice)
        {
            playerCurrency -= item.itemPrice;
            UpdateCurrencyUI();
            inventoryShop.AddItemToInventory(item.itemIcon);
            // Logika untuk menambahkan item ke inventory player bisa ditambahkan di sini
            Debug.Log("Purchased: " + item.itemName);
        }
        else
        {
            Debug.Log("Not enough currency to buy: " + item.itemName);
        }
    }

    // Update UI mata uang
    void UpdateCurrencyUI()
    {
        currencyText.text = playerCurrency.ToString();
    }

    // Fungsi untuk menutup panel konfirmasi (misalnya, jika user menekan tombol batal)
    public void CloseConfirmationPanel()
    {
        confirmationPanel.SetActive(false);
    }

    public void CloseShop()
    {
        cameraShop.SetActive(false);
        shopPanel.SetActive(false);
        virtualCamera.SetActive(true);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
