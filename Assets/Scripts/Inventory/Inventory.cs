using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private List<Item> inventoryList;
    private List<InventorySlot> inventorySlotsUI;
    [SerializeField] private int maxInventorySize = 20;
    [SerializeField] Transform invSlotTransform;
    [SerializeField] Transform inventoryUIParent;

    public CombineSystem combineSystem;
    public Item test1;
    public Item test2;
    public Item test3;
    public Item test4;
    public Item test5;
        
    private void Awake()
    {
        inventoryList = new List<Item>();
        inventoryList.Clear();
        inventorySlotsUI = new List<InventorySlot>();
        inventorySlotsUI.Clear();
    }
    private void Start()
    {
        foreach (Transform child in invSlotTransform)
        {
            InventorySlot slot = child.GetComponent<InventorySlot>();
            if (slot != null)
            {
                inventorySlotsUI.Add(slot);
            }
        }
        inventoryUIParent.gameObject.SetActive(false);
        AddItem(test1);
        AddItem(test2);  
        AddItem(test3);
        AddItem(test4);
        AddItem(test5);
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

    public void CombineItems()
    {
        if (!combineSystem.readyToCombine)
            return;

        //remove combined items from inventory
        for (int i = inventoryList.Count - 1; i >= 0; i--)
        {
            var item = inventoryList[i];

            if (item is CombinableItem combinableItem &&
                combinableItem.AddedToCombine)
            {
                RemoveItem(item); // safe now
            }
        }

        //reset all slot UIs
        foreach (var slot in inventorySlotsUI)
        {
            slot.ResetSlotSelectedUI();
        }

        //add the result item
        Item result = combineSystem.combinedItem;
        AddItem(result);

        combineSystem.ResetCombineSystem();
    }
    public void ResetUI()
    {
        foreach (var slot in inventorySlotsUI)
        {
            slot.ResetSlotSelectedUI();
        }
        RefreshUI();
    }

    public bool ContainsItem(string name)
    {
        foreach(Item i in inventoryList)
        {
            if(i.name == name)
            {
                return true;
            }
        }
        return false;
    }
}

