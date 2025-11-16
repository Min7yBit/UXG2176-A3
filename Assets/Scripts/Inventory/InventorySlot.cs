using UnityEngine.UI;
using UnityEngine;

public class InventorySlot : MonoBehaviour
{
    private Inventory inventory;
    public Item currentItem; // The item in this slot
    public Image icon;       // UI Image showing the item

    private void Awake()
    {
        inventory = GetComponentInParent<Inventory>();
    }

    public void SetItem(Item item)
    {
        currentItem = item;

        if (item != null)
        {
            icon.sprite = item.icon;
            icon.enabled = true;
        }
        else
        {
            icon.sprite = null;
            icon.enabled = false;
        }
    }

    // Called when the player clicks the slot
    public void OnClick()
    {
        if (currentItem != null)
        {
            Debug.Log("Clicked on item: " + currentItem.itemName);
            // Here you can return the item to the combine system
            //InventoryManager.Instance.SelectItem(currentItem);
        }
    }
}
