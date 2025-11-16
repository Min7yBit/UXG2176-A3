using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private List<Item> inventoryList;
    private List<InventorySlot> inventorySlotsUI;
    [SerializeField] private int maxInventorySize = 20;

    public Item test1;
    public Item test2;
    private void Awake()
    {
        inventoryList = new List<Item>();
        inventoryList.Clear();
        inventorySlotsUI = new List<InventorySlot>();
        inventorySlotsUI.Clear();
    }
    private void Start()
    {
        foreach (Transform child in transform)
        {
            InventorySlot slot = child.GetComponent<InventorySlot>();
            if (slot != null)
            {
                inventorySlotsUI.Add(slot);
            }
        }
        AddItem(test1);
        AddItem(test2);  
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RefreshUI();
        }
    }
    public void AddItem(Item newItem)
    {
        if (inventoryList.Count >= maxInventorySize)
        {
            Debug.Log("Inventory is full!");
            return;
        }
        inventoryList.Add(newItem);
        Debug.Log($"Added {newItem.itemName} to inventory.");
        RefreshUI();
    }
    public void RemoveItem(Item itemToRemove)
    {
        if (inventoryList.Remove(itemToRemove))
        {
            Debug.Log($"Removed {itemToRemove.itemName} from inventory.");
            return;
        }
        Debug.Log($"{itemToRemove.itemName} not found in inventory.");
    }
    private void RefreshUI()
    {
        Debug.Log("Refreshing Inventory UI...");
        //clear all slots first
        foreach (var slot in inventorySlotsUI)
        {
            slot.SetItem(null);
        }

        //populate slots with current items
        for (int i = 0; i < inventoryList.Count && i < inventorySlotsUI.Count; i++)
        {
            inventorySlotsUI[i].SetItem(inventoryList[i]);
        }
    }
}

