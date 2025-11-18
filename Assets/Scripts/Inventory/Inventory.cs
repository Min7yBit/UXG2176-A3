using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private List<Item> inventoryList;
    private List<InventorySlot> inventorySlots;
    [SerializeField] private int maxInventorySize = 8;
    [SerializeField] Transform invSlotTransform;
    [SerializeField] Transform inventoryUIParent;
    [SerializeField] private int currentSelectedSlotIndex = 0;

    //public CombineSystem combineSystem;
    public Item test1;
    public Item test2;
    public Item test3;
    public Item test4;
    public Item test5;
        
    private void Awake()
    {
        inventoryList = new List<Item>();
        inventoryList.Clear();
        inventorySlots = new List<InventorySlot>();
        inventorySlots.Clear();
    }
    private void Start()
    {
        foreach (Transform child in invSlotTransform)
        {
            InventorySlot slot = child.GetComponent<InventorySlot>();
            if (slot != null)
            {
                inventorySlots.Add(slot);
            }
        }
        //inventoryUIParent.gameObject.SetActive(false);
        AddItem(test1);
        AddItem(test2);  
        AddItem(test3);
        AddItem(test4);
        AddItem(test5);

        inventorySlots[currentSelectedSlotIndex].SelectToggle(); //initiate first slot as selected
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RefreshUI();
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (currentSelectedSlotIndex >= maxInventorySize - 1) //for resetting to first slot
            {
                inventorySlots[currentSelectedSlotIndex].SelectToggle();
                currentSelectedSlotIndex = 0;
                inventorySlots[currentSelectedSlotIndex].SelectToggle();
            }
            else
            {
                inventorySlots[currentSelectedSlotIndex++].SelectToggle();
                inventorySlots[currentSelectedSlotIndex].SelectToggle();
            }

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
        foreach (var slot in inventorySlots)
        {
            slot.SetItem(null);
        }

        //populate slots with current items
        for (int i = 0; i < inventoryList.Count && i < inventorySlots.Count; i++)
        {
            inventorySlots[i].SetItem(inventoryList[i]);
        }
    }

/*    public void CombineItems()
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
    }*/

    public bool ContainsItem(string name)
    {
        foreach(InventorySlot i in inventorySlots)
        {
            if(i.isSelected == true && i.currentItem.itemName == name)
            {
                return true;
            }
        }
        return false;
    }
}

