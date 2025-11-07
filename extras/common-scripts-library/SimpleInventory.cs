using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Simple Inventory System with item management and UI integration
/// </summary>
public class SimpleInventory : MonoBehaviour
{
    [Header("Inventory Settings")]
    public int maxSlots = 20;
    public bool allowDuplicates = true;

    [Header("UI References")]
    public Transform inventoryPanel;
    public GameObject slotPrefab;

    private List<InventoryItem> items = new List<InventoryItem>();
    private List<GameObject> slotObjects = new List<GameObject>();

    [System.Serializable]
    public class InventoryItem
    {
        public string itemName;
        public int quantity;
        public Sprite itemIcon;
        public GameObject itemPrefab;

        public InventoryItem(string name, int qty, Sprite icon = null, GameObject prefab = null)
        {
            itemName = name;
            quantity = qty;
            itemIcon = icon;
            itemPrefab = prefab;
        }
    }

    void Start()
    {
        InitializeInventory();
    }

    void InitializeInventory()
    {
        if (inventoryPanel != null && slotPrefab != null)
        {
            // Create inventory slots
            for (int i = 0; i < maxSlots; i++)
            {
                GameObject slot = Instantiate(slotPrefab, inventoryPanel);
                slotObjects.Add(slot);
            }
        }
    }

    public bool AddItem(string itemName, int quantity = 1, Sprite icon = null, GameObject prefab = null)
    {
        if (items.Count >= maxSlots && !HasItem(itemName))
        {
            Debug.Log("Inventory is full!");
            return false;
        }

        // Check if item already exists
        InventoryItem existingItem = GetItem(itemName);
        if (existingItem != null && allowDuplicates)
        {
            existingItem.quantity += quantity;
        }
        else
        {
            items.Add(new InventoryItem(itemName, quantity, icon, prefab));
        }

        UpdateInventoryUI();
        return true;
    }

    public bool RemoveItem(string itemName, int quantity = 1)
    {
        InventoryItem item = GetItem(itemName);
        if (item == null || item.quantity < quantity)
        {
            return false;
        }

        item.quantity -= quantity;
        if (item.quantity <= 0)
        {
            items.Remove(item);
        }

        UpdateInventoryUI();
        return true;
    }

    public bool HasItem(string itemName)
    {
        return GetItem(itemName) != null;
    }

    public int GetItemQuantity(string itemName)
    {
        InventoryItem item = GetItem(itemName);
        return item != null ? item.quantity : 0;
    }

    public InventoryItem GetItem(string itemName)
    {
        return items.Find(item => item.itemName == itemName);
    }

    public List<InventoryItem> GetAllItems()
    {
        return new List<InventoryItem>(items);
    }

    public void ClearInventory()
    {
        items.Clear();
        UpdateInventoryUI();
    }

    void UpdateInventoryUI()
    {
        if (inventoryPanel == null) return;

        // Clear existing UI
        foreach (GameObject slot in slotObjects)
        {
            slot.SetActive(false);
        }

        // Update UI with current items
        for (int i = 0; i < items.Count && i < slotObjects.Count; i++)
        {
            GameObject slot = slotObjects[i];
            slot.SetActive(true);

            // Update slot content
            var slotScript = slot.GetComponent<InventorySlot>();
            if (slotScript != null)
            {
                slotScript.SetItem(items[i]);
            }
        }
    }

    public void SaveInventory()
    {
        // Save inventory to PlayerPrefs
        string json = JsonUtility.ToJson(new InventoryData { items = items });
        PlayerPrefs.SetString("Inventory", json);
    }

    public void LoadInventory()
    {
        if (PlayerPrefs.HasKey("Inventory"))
        {
            string json = PlayerPrefs.GetString("Inventory");
            InventoryData data = JsonUtility.FromJson<InventoryData>(json);
            items = data.items;
            UpdateInventoryUI();
        }
    }

    [System.Serializable]
    public class InventoryData
    {
        public List<InventoryItem> items;
    }
}

/// <summary>
/// Inventory Slot UI Component
/// </summary>
public class InventorySlot : MonoBehaviour
{
    public UnityEngine.UI.Image itemIcon;
    public UnityEngine.UI.Text itemName;
    public UnityEngine.UI.Text itemQuantity;

    public void SetItem(SimpleInventory.InventoryItem item)
    {
        if (itemIcon != null)
            itemIcon.sprite = item.itemIcon;

        if (itemName != null)
            itemName.text = item.itemName;

        if (itemQuantity != null)
            itemQuantity.text = item.quantity > 1 ? item.quantity.ToString() : "";
    }
}
